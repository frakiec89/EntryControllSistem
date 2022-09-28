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
        private int _acauntId;
        private DB.Acaunt _Acaunt;
        private bool isStartFlag = false;
        public bool isSafe = false; // если надо  обновить контент  стартовой страницы 
        public UserAcauntingWindows(int  acaunt)
        {
            InitializeComponent();
            _acauntId = acaunt;
            this.Loaded += UserAcauntingWindows_Loaded;
        }

        private void UserAcauntingWindows_Loaded(object sender, RoutedEventArgs e)
        {
            btnSave.Visibility = Visibility.Collapsed;

            DB.MyContext myContext = new DB.MyContext();
            try
            {
                _Acaunt = myContext.Acaunts.Single(x => x.AcauntId == _acauntId);
                this.Title = $"Редактировать  профиль {_Acaunt.Name}";
                imageAcaunt.Source = GetImage(_Acaunt.PathImage);
                tbName.Text = _Acaunt.Name;
                dataGridAcauntimg.ItemsSource = myContext.EntryControls.Where(x=> x.AcauntId == _acauntId).
                    OrderBy(x=>x.DateTimeEntryControl).ToList();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private ImageSource GetImage(string pathImage)
        {
          Uri uri = new Uri(@"pack://application:,,,/AcauntImage/" +   pathImage );
            var bm = new BitmapImage(uri);
            return bm;
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _Acaunt.Name = tbName.Text;
            
            DB.MyContext myContext = new DB.MyContext();
            try
            {
                myContext.Acaunts.Update(_Acaunt);
                myContext.SaveChanges();
                isStartFlag = false;
                isSafe = true;
                MessageBox.Show("Провель  пересохранен  в  базе  данных");
                UserAcauntingWindows_Loaded(null, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(isStartFlag== false)
            {
                isStartFlag = true;
                return;
            }
            if (btnSave.Visibility == Visibility.Collapsed)
                btnSave.Visibility = Visibility.Visible;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            var resDialog =  MessageBox.Show("Вы увееры  что  хотите  удалить  записи ?",
                "Внимание!!", MessageBoxButton.YesNo, MessageBoxImage.Question);
         
            if (resDialog == MessageBoxResult.No)
                return; 
            try
            {
                var entrySelect = dataGridAcauntimg.SelectedItems;
                MyContext myContext = new MyContext();
                List< DB.EntryControl> removList =new List< DB.EntryControl>();
                foreach (var row in entrySelect)
                {
                    DB.EntryControl entryControl = row as DB.EntryControl;
                   
                    if(entryControl!=null)
                    {
                        removList.Add(entryControl);
                    }
                }
                myContext.EntryControls.RemoveRange(removList);
                myContext.SaveChanges();


                MessageBox.Show("все записи удалены ");
            }
            catch (Exception ex)
            {
                MessageBox.Show("ошибка удаления из базы данных \n" + ex.Message);
            }
            finally
            {
                isStartFlag = false;
                isSafe = true;
                UserAcauntingWindows_Loaded(null, null);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

            MyForms.ImageBox imageBox = new ImageBox();
          
            if (  imageBox.ShowDialog() == true )
            {
                DB.MyContext myContext = new DB.MyContext();
                try
                {
                    _Acaunt.PathImage = imageBox.SelectImage.Name;
                    myContext.Acaunts.Update(_Acaunt);
                    myContext.SaveChanges();
                    isSafe = true;
                    UserAcauntingWindows_Loaded(null, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btDellAccaunt_Click(object sender, RoutedEventArgs e)
        {
           var dialog =   MessageBox.Show($"Вы уверены что вы  хотите удалить  пользователя? \n" +
                $" Все истории  входа  пользователя {_Acaunt.Name} будут  удалены" , "Важно!!!" , MessageBoxButton.OKCancel , MessageBoxImage.Warning);

            if (dialog == MessageBoxResult.OK)
            {
                DB.MyContext myContext = new DB.MyContext();
                try
                {

                    myContext.EntryControls.RemoveRange(myContext.EntryControls.Where(x => x.AcauntId == _acauntId));
                    myContext.Acaunts.Remove(_Acaunt);
                    myContext.SaveChanges();
                    isSafe = true;
                    MessageBox.Show("Пользователь  удален !!!");
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
