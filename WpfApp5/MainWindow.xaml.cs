using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp5.ModelView;

namespace WpfApp5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Переменая  хранит  контент  о  входах из  бд ... служит  как  буферная память  
        /// </summary>
        List<ModelView.EntryControlView> entries ; 

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded; // действия при  старте  системы
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var entryControlController = new Controllers.EntryControlController(); // создали контроллер  для получения из бд
            entries = new List<EntryControlView>(); // выделим  память  под  буфер 
            entries = entryControlController.GetEntryControlTheLastFiveDays(); // получили  контент из  БД за счет  контролера 
            listContent.ItemsSource = entries; // даем  контент лист боксу 
            GetCountForLabel(); // получаем кол-во  записей  
            cbNoSorting.IsChecked = true; // выставляем фильтр без сортировки 
        }


        /// <summary>
        /// сортирует  контент  ... работает  совместно  с  поиском 
        /// </summary>
        private void SortContent()
        {
            var content = listContent.ItemsSource as IEnumerable< EntryControlView> ; // заберем контент  из листбокса 

            if (cbNoSorting.IsChecked == true) // если без сортировки  
            {
                listContent.ItemsSource = content.OrderBy(x => x.IdAccaunt); // отсортируем контент  по  ид  пользователя 
                return; // выйдем  
            }

            if (cbUpName.IsChecked == true) // если  по  возрастанию 
            {
                listContent.ItemsSource = content.OrderBy(x=>x.Name); // // отсортируем контент  по  имени 
                return;
            }

            if (cbDown.IsChecked == true) // если по  убыванию 
            {
                listContent.ItemsSource = content.OrderByDescending(x => x.Name);// отсортируем контент  по  имени
                return;
            }
        }

        /// <summary>
        /// получаем контент  для лейбла  - кол-во  записей  
        /// </summary>
        private void GetCountForLabel()
        {
            lbCountList.Content =  $"Пользователей в списке: {listContent.Items.Count}"; // получаем  кол-во  записей  в  листбоксе 
        }

        /// <summary>
        /// Обработка  событий  добавления  нового  пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            MyForms.AddAcauntWindows addAcauntWindows = new MyForms.AddAcauntWindows(); // создаем новое  окно
            if (addAcauntWindows.ShowDialog() == true) // выводим окно  на  экран  - если результат  диалога  истина 
                MainWindow_Loaded(null, e); // обновим  форму   - вызовем метод MainWindow_Loaded
                                            // - дадим  ему  пустой  sender ,  и любой RoutedEventArgs
                                            // для того  что  бы  соблюсти  сигнатуру  метода 
        }

        /// <summary>
        /// случайный  вход
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRandomEntry_Click(object sender, RoutedEventArgs e)
        {

            DB.MyContext myContext = new DB.MyContext(); // подключение  к  бд 
            try
            {
                Random random = new Random(); 
                var randomIdlist = myContext.Acaunts.Select(x => x.AcauntId).ToList(); // выборка  всех пользователей  в  бд  из таблицы  Acaunts  -> в массив 
                int id = randomIdlist[random.Next(0, randomIdlist.Count)]; // найдем случайного  пользователя  из  массива 
                var randomUser = myContext.Acaunts.Single(x => x.AcauntId == id );  // найдем пользователя   в  бд  по  его  ид 
              
                var randomData = DateTime.Now; // текущая дата  в формате   28.09.2022 12:40:16
                randomData = randomData.AddDays(-random.Next(0, 5)).AddHours(-random.Next(0,24)).AddMinutes(random.Next(0,59)); //  случайная дата  в пределах 6 дней 
                 
                var newEntry = new DB.EntryControl() // создаем  новый объект  для таблицы  EntryControl в бд
                {
                    AcauntId = randomUser.AcauntId, // дадим ему  выбраны  ид пользователя 
                    DateTimeEntryControl = randomData // дадим  ему  случайную дату  
                };
                myContext.EntryControls.Add(newEntry); // добавим новый  вход  в  бд
                myContext.SaveChanges(); // сохраним изменения в  бд 
                MessageBox.Show($"Пользователь  {randomUser.Name} совершил вход  в {newEntry.DateTimeEntryControl}"); // оповестим  пользователя 
                MainWindow_Loaded(null, e); // загрузим форму заново 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // если  ошибка  
            }
        }

        /// <summary>
        /// Переход  в профиль  пользователя  через кнопку 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoToAcauntClick(object sender, RoutedEventArgs e)
        {
            ModelView.EntryControlView acaunt; // переменная  что  бы  понять на какого  пользователя перейти  - пока пусто 
            if (sender is ModelView.EntryControlView) // если  пользователь  известен  // событие произошло при нажатии  контекстного меню или двойного клика 
            {
                acaunt = sender as ModelView.EntryControlView; // забираем его  из сендер 
            }
            else // если событие  произошло при нажатии  на кнопку 
            {
                var b = e.OriginalSource as Button; //  получаем  кнопку 
                acaunt = b.DataContext as ModelView.EntryControlView; // забираем  пользователя  из контекста  кнопки 
            }

            if (acaunt == null) // если  не  получилось  получить пользователя 
                return; // просто  выйдем  

            MyForms.UserAcauntingWindows windows =  new MyForms.UserAcauntingWindows(acaunt.IdAccaunt); // создаем новое окно  - передаем  в  него  ид  пользователя 
            windows.ShowDialog(); // вызываем окно 
            
            if(windows.isSafe == true) // если профиль  изменился 
                MainWindow_Loaded(null, new RoutedEventArgs()); // перегрузим окно  MainWindow
        }


        /// <summary>
        /// поиск в списке 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbSource_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(string.IsNullOrEmpty(tbSource.Text)) // если пустой 
                listContent.ItemsSource = entries; // берем контен  из буфера 

            var res = entries.Where(x => x.Name.ToUpper().Contains(tbSource.Text.ToUpper()));  // ищем колекцию   где есть  совпадения 

            if (res.Count()>0) // если объекты найдены 
            {
                listContent.ItemsSource = res; // даем  новый контент  в  листбокс 
            }
            else
                MessageBox.Show("Пользователь  с таким  именем не найден"); //

            cbNoSorting.IsChecked = true; // сбросим  фильтр  
            GetCountForLabel(); // получим  новый контент  кол-ва записей 
        }

        /// <summary>
        /// событие  срабатывание если меняется сортировка 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbNoSorting_Checked(object sender, RoutedEventArgs e)
        {
            SortContent();  // вызываем метод 
        }

        /// <summary>
        /// событие двойное   нажатие  мышкой  по  листбоксу  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listContent_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var  acaunt    = listContent.SelectedItem as ModelView.EntryControlView;  // находим выбранного  пользователя  в листбоксе 
            GoToAcauntClick(acaunt, e); // вызываем метод  - как будто  нажали на кнопку  - в  сендер  передадим  акаунт 
        }

        /// <summary>
        /// событие  контекстного меню 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var user = listContent.SelectedItem as ModelView.EntryControlView; // находим выбранного  пользователя  в листбоксе 
            GoToAcauntClick(user, e); // вызываем метод  - как будто  нажали на кнопку  - в  сендер  передадим  акаунт 
        }
    }
}
