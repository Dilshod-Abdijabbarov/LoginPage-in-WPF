using LoginTask.Models;
using System;
using System.Linq;
using System.Windows;

namespace LoginTask.Pages
{
    public partial class UpdatePage : Window
    {
        DatabaseContext dbContext;
        int Id;
        public UpdatePage(int userId)
        {
            InitializeComponent();
            this.dbContext = new DatabaseContext();
            Id = userId;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var users = dbContext.Users.ToList();
            string uservalues = textboxUser.Text;
            string passwordvalues =textBoxPassword.Text;

            var userUpdateName = users.FirstOrDefault(x => x.Name.Equals(uservalues));
            var userUpdatePassword = users.FirstOrDefault(x => x.Password.Equals(GenerateHash(passwordvalues)));

            var updateuser=dbContext.Users.FirstOrDefault(x => x.Id == Id);

            if (uservalues.Length < 8)
            {
                MessageBox.Show("UserName kamida 8 ta belgidan iborat bo'lishi kerak!");
            }
            else if (passwordvalues.Length < 8)
            {
                MessageBox.Show("Password kamida 8 ta belgidan iborat bo'lishi kerak!");
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
                        else if(userUpdateName == null || userUpdatePassword==null ||
                                    userUpdateName != null && userUpdatePassword != null ||
                                    userUpdateName == null && userUpdatePassword != null)
                        {
                            updateuser.Name = textboxUser.Text;
                            updateuser.Password = GenerateHash(passwordvalues);
                            dbContext.SaveChanges();
                            Dashboard.dataGrid.ItemsSource = dbContext.Users.ToList();
                            this.Hide();
                        }   
                        else
                        {
                            MessageBox.Show("Bu User Name allaqachon mavjud!");
                        }
                    }
                }
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Id == Id);
            textboxUser.Text = user.Name;
            textBoxPassword.Text = GetStringText(user.Password);
        }

        public static string GetStringText(string password)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(password);
            return System.Text.Encoding.Unicode.GetString(base64EncodedBytes);
        }

        public static string GenerateHash(string password)
        {
            byte[] buffer = System.Text.Encoding.Unicode.GetBytes(password);
            return System.Convert.ToBase64String(buffer);
        }
    }
}
