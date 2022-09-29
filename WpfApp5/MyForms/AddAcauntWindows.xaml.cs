using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using WpfApp5.Controllers;
using WpfApp5.ModelView;
using static System.Net.WebRequestMethods;

namespace WpfApp5.MyForms
{
    /// <summary>
    /// Логика взаимодействия для AddAcauntWindows.xaml
    /// </summary>
    public partial class AddAcauntWindows : Window
    {
        public AddAcauntWindows()
        {
            InitializeComponent();
            this.Loaded += AddAcauntWindows_Loaded;
        }

        private void AddAcauntWindows_Loaded(object sender, RoutedEventArgs e)
        {
            cbImage.ItemsSource = ImageController.GetImage(); // получам картинки  из контроллера  в  комбобокс 
            cbDepatment.ItemsSource = GetDepetmant();
        }

        private List<DB.Department> GetDepetmant()
        {
            DB.MyContext myContext = new DB.MyContext(); // подключение  к  бд 
            try
            {
               return   myContext.Departments.OrderBy(x => x.Name).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        /// <summary>
        /// добавить пользователя  в  бд
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            DB.MyContext myContext = new DB.MyContext(); // подключение  к  бд 
            try
            {
                var newAcaunt = new DB.Acaunt(); // создаем нового  пользователя 
                if (string.IsNullOrEmpty(tbName.Text)) // если пустое  имя 
                {
                    MessageBox.Show("Укажите имя");
                    return;
                }


                var depapment = cbDepatment.SelectedItem as DB.Department;

                if(depapment==null)
                {
                    MessageBox.Show("Выберите отдел"); // иначе  выведем  пользователю 
                    return; //выйдем 
                }

                newAcaunt.DepartmentId = depapment.DepartmentId;
                newAcaunt.Name = tbName.Text; // имя для него  из  текстбокса 
                var image = cbImage.SelectedItem as ModelImage; // картинка из  комбобокса 
                if (image != null) // если  не  нулевая  картинка 
                newAcaunt.PathImage = image.Name; // акаунт  получает  название картинки  - будет  хронить ее  в бд 
                else
                {
                    MessageBox.Show("Выберите картинку"); // иначе  выведем  пользователю 
                    return; //выйдем 
                }
                myContext.Acaunts.Add(newAcaunt); // добавим акаунт в  бд  в  таблицу  Acaunts
                btnAddUser.IsEnabled = false; // заблокируем  кнопку - что бы не  кто не нажал  два раза 
                myContext.SaveChanges(); // отправим  в  бд 
                MessageBox.Show("объект  добавлен  в  БД");
                DialogResult = true;  // закроем  окно  с  диалогом true
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btnAddUser.IsEnabled = true; // в  случае ошибки   откроем кнопку  
            }
        }
    }

   
}
