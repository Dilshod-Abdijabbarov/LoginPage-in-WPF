using LoginTask.Models;
using System;
using System.Linq;
using System.Windows;

namespace LoginTask.Pages
{
    public partial class Login : Window
    {
        private readonly DatabaseContext dbContext;
        int id;
        public Login(int Id)
        {
            InitializeComponent();
            this.dbContext = new DatabaseContext();
            this.id = Id;           
        }
        MainWindow mainWindow { get => Application.Current.MainWindow as MainWindow; }

        private void HandleCheck(object sender, RoutedEventArgs e)
        {
        }

        private void HandleUnchecked(object sender, RoutedEventArgs e)
        {
        }

        private void Button_ClickRegis(object sender, RoutedEventArgs e)
        {
            mainWindow.mainFrame.Navigate(new Uri("/pages/Registration.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {          
            var users = dbContext.Users.ToList();
            var user = users.FirstOrDefault(x => x.Id == id);

            string uservalues = userName.Text;
            var passwordvalues = passwordText.passbox.Password;
            var passwordbyte = ToBase64(passwordvalues);

            var username = users.FirstOrDefault(x => x.Name.Equals(userName.Text));
            var password = users.FirstOrDefault(x => x.Password.Equals(passwordbyte));

            if (uservalues.Length<8)
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
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Bunaqa foydalanuvchi topilmadi!");
                        }
                    }
                }
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            var user=dbContext.Users.FirstOrDefault(x=>x.Id==id);
            var passwordvalues = user.Password;
            userName.Text = user.Name;
            passwordText.passbox.Password = GetStringText(passwordvalues);
        }
        public static string GetStringText(string password)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(password);
            return System.Text.Encoding.Unicode.GetString(base64EncodedBytes);
        }
        public static string ToBase64(string s)
        {
            byte[] buffer = System.Text.Encoding.Unicode.GetBytes(s);
            return System.Convert.ToBase64String(buffer);
        }
    }
}
