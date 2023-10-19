using LoginTask.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LoginTask.Pages
{
    public partial class LoginPage : Page
    {
        private readonly DatabaseContext dbContext;
        int id;
        MainWindow mainWindow { get => Application.Current.MainWindow as MainWindow; }

        public LoginPage()
        {
            InitializeComponent();
            this.dbContext = new DatabaseContext();
        }

        private void TextBoxWithPlaceHolder_Loaded(object sender, RoutedEventArgs e)
        {
            //var ss = cache.Get(id);
            //userName.Text = ss.Name;
            //passwordText.Text = ss.Password;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var users = dbContext.Users.ToList();
            
            string uservalues = userName.Text;
            string passwordvalues = passwordText.passbox.Password;
            string passwordhash = GetStringText(passwordvalues);

            var username = users.FirstOrDefault(x => x.Name.Equals(userName.Text));
            var password = users.FirstOrDefault(x => x.Password.Equals(passwordhash));

            if (uservalues.Length < 8)
            {
                MessageBox.Show("UserName 8 ta belgidan kam bo'lmasligi kerak!");
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
                        else if (username != null && password != null)
                        {
                            mainWindow.mainFrame.Navigate(new Uri("/pages/Dashboard.xaml", UriKind.RelativeOrAbsolute));
                        }
                        else
                        {
                            MessageBox.Show("Bunday foydalanuvchi topilmadi!");
                        }
                    }
                }
            }
        }

        private void Button_ClickRegis(object sender, RoutedEventArgs e)
        {
            mainWindow.mainFrame.Navigate(new Uri("/pages/Registration.xaml", UriKind.RelativeOrAbsolute));
        }  

        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            //var data = dbContext.Users.FirstOrDefault(x => x.Id == id);         

            //var cacheOptions = new MemoryCacheEntryOptions()
            //    .SetAbsoluteExpiration(TimeSpan.FromMinutes(8));

            //cache.Set(id, data, cacheOptions);
        }
        private void HandleUnchecked(object sender, RoutedEventArgs e)
        {
        }
        public static string GetStringText(string password)
        {
            byte[] buffer = System.Text.Encoding.Unicode.GetBytes(password);
            return System.Convert.ToBase64String(buffer);
        }
    }
}
