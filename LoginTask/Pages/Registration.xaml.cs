using LoginTask.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using DurableTask.Core.Common;

namespace LoginTask.Pages
{
    public partial class Registration : Page
    {
        private readonly DatabaseContext dbContext;
        MainWindow mainWindow { get => Application.Current.MainWindow as MainWindow; }

        public Registration()
        {
            InitializeComponent();
            this.dbContext = new DatabaseContext();
        }
        User userAdd = new User();
        private void Button_ClickRegistr(object sender, RoutedEventArgs e)
        {
            var users = dbContext.Users.ToList();
            string passwordvalues = passwordText.passbox.Password;
            string uservalues = userName.Text;
            var user = users.FirstOrDefault(x => x.Name.Equals(userName.Text));
            var password = users.FirstOrDefault(x => x.Password.Equals(passwordvalues));

            if (uservalues.Length<8)
            {
                MessageBox.Show("UserName 8 ta belgidan kam bo'lmasligi kerak!");
            }
            else if (passwordvalues.Length < 8)
            {
                MessageBox.Show("Password kamida 8 ta belgidan iborat bo'lishi kerak!");
            }
            else if (user != null)
            {
                MessageBox.Show("Bunday UserName allaqachon mavjud!");
            }
            else
            {
                int l = 0, t = uservalues.Length;

                for (int i = 0; i < uservalues.Length; i++)
                {
                    if ((int)uservalues[i] >= 97 && (int)uservalues[i] <= 122 || (int)uservalues[i] >= 65 && (int)uservalues[i] <= 90 || (int)uservalues[i] >= 48 && (int)uservalues[i] <= 57)
                    {
                        l++;
                    }
                }

                if (t != l)
                {
                    MessageBox.Show("Kiritgan User Name malumotingiz Krillchada bo'lishi mumkin tekshirib qaytadan Lotinchada kiriting!");
                }
                else
                {
                    int n = 0, m = passwordvalues.Length;

                    for (int i = 0; i < passwordvalues.Length; i++)
                    {
                        if ((int)passwordvalues[i] >= 97 && (int)passwordvalues[i] <= 122 || (int)passwordvalues[i] >= 65 && (int)passwordvalues[i] <= 90 || (int)passwordvalues[i] >= 48 && (int)passwordvalues[i] <= 57)
                        {
                            n++;
                        }
                    }

                    if (m != n)
                    {
                        MessageBox.Show("Kiritgan Password malumotingiz Krillchada bo'lishi mumkin tekshirib qaytadan Lotinchada kiriting!");
                    }
                    else
                    {
                        if (!(passwordvalues is string Password && Password.Length >= 8 &&
                               Password.Any(c => char.IsDigit(c)) &&
                                   Password.Any(c => char.IsLetter(c)) &&
                                       Password.Any(c => char.IsUpper(c))))
                        {
                            MessageBox.Show("Password kiritayotganda kamida bitta katta harf va raqam qatnashish shart! Iltimos tekshirib qayta passwordni kiriting.");
                        }
                        else
                           if (passwordvalues != passwordCorrentText.passbox.Password)
                        {
                            MessageBox.Show("Passwordni qayta kiritganingizda xatolikka yo'l qo'ydingiz!");
                        }
                        else
                       {
                            var hashedPassword=GenerateHash(passwordvalues);                         

                            var count = users.Max(x => x.Id);
                            count++;
                            userAdd.Id = count;
                            userAdd.Name = userName.Text;
                            userAdd.Password = hashedPassword;
                            dbContext.Users.Add(userAdd);

                            dbContext.SaveChanges();

                            MessageBox.Show("Ro'yxatdan muvofiqayatli o'tdingiz!!!");

                            Login login = new Login(userAdd.Id);
                            login.ShowDialog();
                        }
                    }
                }
            }

        }
        public static string GenerateHash(string password)
        {
            byte[] buffer = System.Text.Encoding.Unicode.GetBytes(password);
            return System.Convert.ToBase64String(buffer);
        }

    }
}
