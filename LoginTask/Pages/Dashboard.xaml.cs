using LoginTask.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LoginTask.Pages
{
    public partial class Dashboard : Page
    {
        private readonly DatabaseContext dbContext;
        public static DataGrid dataGrid;
        public Dashboard()
        {
            InitializeComponent();
            this.dbContext = new DatabaseContext();
        }
      User user=new User();

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            int id = (myDataGrid.SelectedItem as User).Id;
            var isdelete=dbContext.Users.Where(x => x.Id == id).Single();
            dbContext.Users.Remove(isdelete);
            dbContext.SaveChanges();
            myDataGrid.ItemsSource = dbContext.Users.ToList();
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            int id=(myDataGrid.SelectedItem as User).Id;
            UpdatePage updatePage = new UpdatePage(id);
            updatePage.ShowDialog();
        }

        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Clickusers(object sender, RoutedEventArgs e)
        {

        }

        private void myDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            myDataGrid.ItemsSource = dbContext.Users.ToList();

            dataGrid = myDataGrid;
        }

        private void Button_Click_Inbox(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
