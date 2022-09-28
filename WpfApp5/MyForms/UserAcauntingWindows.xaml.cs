using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp5.DB;

namespace WpfApp5.MyForms
{
    /// <summary>
    /// Логика взаимодействия для UserAcauntingWindows.xaml
    /// </summary>
    public partial class UserAcauntingWindows : Window
    {
        /// <summary>
        /// ид  пользователя  - приходит  через конструктор  
        /// </summary>
        private int _acauntId;
       
        /// <summary>
        /// весь пользователь  - находим из БД  по  ид - пригодится  
        /// </summary>
        private DB.Acaunt _Acaunt; 

        /// <summary>
        /// булево значение  - нужно что  бы  понимать  произошло событие  при старте или потом - нужно  для обновлений 
        /// </summary>
        private bool isStartFlag = false;

        /// <summary>
        ///  если надо  обновить контент  стартовой страницы 
        /// </summary>
        public bool isSafe = false; // если надо  обновить контент  стартовой страницы 
       
        public UserAcauntingWindows(int  acaunt)
        {
            InitializeComponent();
            _acauntId = acaunt; // запомнили ид 
            this.Loaded += UserAcauntingWindows_Loaded;
        }

        /// <summary>
        /// стартовый метод 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserAcauntingWindows_Loaded(object sender, RoutedEventArgs e)
        {
            btnSave.Visibility = Visibility.Collapsed; // выключим кнопку сохранения  в  бд  , она нужна  если что  то  измениться 

            DB.MyContext myContext = new DB.MyContext(); // подключение  к  бд
            try
            {
                _Acaunt = myContext.Acaunts.Single(x => x.AcauntId == _acauntId); // нашли пользователя  - пригодиться 
                this.Title = $"Редактировать  профиль {_Acaunt.Name}"; // изменили шапку 
                imageAcaunt.Source = GetImage(_Acaunt.PathImage); // нашли картинку  - по  названю  картинки  из бд 
                tbName.Text = _Acaunt.Name; // задали полу  с  именем  пользователя 
                dataGridAcauntimg.ItemsSource = myContext.EntryControls.Where(x=> x.AcauntId == _acauntId).  
                    OrderBy(x=>x.DateTimeEntryControl).ToList(); // нашли все входы пользователя в  бд  - отсортировали по дате 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// получает  картинку  по  названию 
        /// </summary>
        /// <param name="pathImage">название  файла  из бд - например  ivanov.png </param>
        /// <returns></returns>
        private ImageSource GetImage(string pathImage)
        {
          Uri uri = new Uri(@"pack://application:,,,/AcauntImage/" +   pathImage ); // получили путь 
            var bm = new BitmapImage(uri); // получили битмап 
            return bm; // вернули BitmapImage -> Наследник от ImageSource
        }


        /// <summary>
        /// сохранение в  бд 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _Acaunt.Name = tbName.Text; // получили новое  имя  для пользователя 
            
            DB.MyContext myContext = new DB.MyContext();
            try
            {
                myContext.Acaunts.Update(_Acaunt); // обновили пользователя в бд 
                myContext.SaveChanges(); // сохранили изменение 
                isStartFlag = false; // поменяли флаг  - это  событие  прошло не  при старте 
                isSafe = true; // поменяли флаг   - при закрытие  этого окна нужно  будет  обновить  контент окна  mainWindows 
                MessageBox.Show("Провель  пересохранен  в  базе  данных"); 
                UserAcauntingWindows_Loaded(null, e); // обновили окно 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// проиходит  когда меняется текст  в  поле  имя пользователя 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(isStartFlag== false) // если текст  поменялся  при  старте  
            {
                isStartFlag = true; // запомним это   и выйдем  .. показывать  кнопку "сохранить  в бд"  при  старте  окна  не  надо 
                return; // выход 
            }

            // если не  при  старте  поменялся текст 
            if (btnSave.Visibility == Visibility.Collapsed)   // если кнопка скрыта  
                btnSave.Visibility = Visibility.Visible;// покажем кнопку  
        }

        /// <summary>
        /// обработка  события удаления  записецй 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            var resDialog =  MessageBox.Show("Вы уверены  что  хотите  удалить  записи ?", //  спросим  у пользователя 
                "Внимание!!", MessageBoxButton.YesNo, MessageBoxImage.Question);
         
            if (resDialog == MessageBoxResult.No) // если нет  то  выйдем 
                return;  // выход 

            try
            {
                var entrySelect = dataGridAcauntimg.SelectedItems; // получим выделенные  записи  из  таблицы 
                MyContext myContext = new MyContext();

                List< DB.EntryControl> removList =new List< DB.EntryControl>(); // создадим переменую - буду  складывать  все что надо удалить
               
                foreach (var row in entrySelect) // перебираемым  выделенные  строки 
                {
                    DB.EntryControl entryControl = row as DB.EntryControl; // преобразуем строку  в  EntryControl 

                    if (entryControl!=null) // если не  нулевой 
                    {
                        removList.Add(entryControl); // добавим  объект  в  removList
                    }
                }
                myContext.EntryControls.RemoveRange(removList); // после перебора  удалим все  входы  из таблицы EntryControls
                myContext.SaveChanges(); // сохраним изменения 

                MessageBox.Show("все записи удалены ");
            }
            catch (Exception ex)
            {
                MessageBox.Show("ошибка удаления из базы данных \n" + ex.Message);
            }
            finally // в  любом случае  
            {
                isStartFlag = false;  // поменяем  флаги 
                isSafe = true; // 
                UserAcauntingWindows_Loaded(null, null); // обновим  страницу 
            }
        }

        /// <summary>
        /// контекстное меню  на  картинке 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MyForms.ImageBox imageBox = new ImageBox(); //создадим  форму  с картинками  
          
            if (  imageBox.ShowDialog() == true) // выведем окно  с картинками // если  диалог  состоялся 
            {
                DB.MyContext myContext = new DB.MyContext(); 
                try
                {
                    _Acaunt.PathImage = imageBox.SelectImage.Name; // поменяем  название  картинки   у  пользователя 
                    myContext.Acaunts.Update(_Acaunt); // обновим базу  даныых 
                    myContext.SaveChanges();
                    isSafe = true; // поменяем  флаг 
                    UserAcauntingWindows_Loaded(null,  e); // обновим  окно  
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Удаление  пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDellAccaunt_Click(object sender, RoutedEventArgs e)
        {
           var dialog =   MessageBox.Show($"Вы уверены что вы  хотите удалить  пользователя? \n" +
                $" Все истории  входа  пользователя {_Acaunt.Name} будут  удалены" , "Важно!!!" ,
                MessageBoxButton.OKCancel , MessageBoxImage.Warning); // спросим  уверен  ли пользователь  

            if (dialog == MessageBoxResult.OK) // если да  
            {
                DB.MyContext myContext = new DB.MyContext();
                try
                {

                    myContext.EntryControls.RemoveRange(myContext.EntryControls.Where(x => x.AcauntId == _acauntId)); // удалим  все входы  у пользователя 
                    myContext.Acaunts.Remove(_Acaunt); // удалим пользователя 
                    myContext.SaveChanges(); // обновимся в бд 
                    isSafe = true; // поменяем  флаг 
                    MessageBox.Show("Пользователь  удален !!!");
                    Close(); // закроем окно   - при  удаленном  пользователе  тут  делать  дольше  нечего 
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}