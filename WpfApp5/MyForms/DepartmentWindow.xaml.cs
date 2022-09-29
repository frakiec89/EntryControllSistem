using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp5.MyForms
{
    /// <summary>
    /// Логика взаимодействия для DepartmentWindow.xaml
    /// </summary>
    public partial class DepartmentWindow : Window
    {

        public DB.Department selectDepetment; 

        public DepartmentWindow()
        {
            InitializeComponent();
            this.Loaded += DepartmentWindow_Loaded;
        }

        private void DepartmentWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DB.MyContext myContext = new DB.MyContext();
                listboxDep.ItemsSource = myContext.Departments.OrderBy(x=>x.Name).ToList();
            }
            catch (Exception)
            {
                throw;
            }
         
        }

        private void btSelect_Click(object sender, RoutedEventArgs e)
        {
            selectDepetment = (DB.Department)listboxDep.SelectedItem;
            DialogResult = true;
        }
    }
}
