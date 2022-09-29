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
    /// Логика взаимодействия для AddDepartamentWindows.xaml
    /// </summary>
    public partial class AddDepartamentWindows : Window
    {
        public AddDepartamentWindows()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show("Введите название отдела");
                return;
            }

            try
            {
                DB.MyContext myContext = new DB.MyContext();
                myContext.Departments.Add(new DB.Department { Name = tbName.Text });
                myContext.SaveChanges();
                MessageBox.Show("Отдел добавлен в базу данных");
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ошибка базы  данных \n" + ex.Message);
            }
        }
    }
}
