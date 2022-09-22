using System;
using System.Collections;
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
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            Controllers.EntryControlController entryControlController = new Controllers.EntryControlController();
            listContent.ItemsSource = entryControlController.GetEntryControlTheLastFiveDays();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            MyForms.AddAcauntWindows addAcauntWindows = new MyForms.AddAcauntWindows();
            if (addAcauntWindows.ShowDialog() == true)
                MainWindow_Loaded(null, e);
        }

        private void btnRandomEntry_Click(object sender, RoutedEventArgs e)
        {

            DB.MyContext myContext = new DB.MyContext();

            try
            {
                Random random = new Random();
                var randomIdlist = myContext.Acaunts.Select(x => x.AcauntId).ToList();
                int id = randomIdlist[random.Next(0, randomIdlist.Count)];
                var randomUser = myContext.Acaunts.Single(x => x.AcauntId == id );
                var randomData = DateTime.Now;
                randomData = randomData.AddDays(-random.Next(0, 5)).AddHours(-random.Next(0,59)).AddMinutes(random.Next(0,59));
                var newEntry = new DB.EntryControl()
                {
                    AcauntId = randomUser.AcauntId,
                    DateTimeEntryControl = randomData
                };
                myContext.EntryControls.Add(newEntry);
                myContext.SaveChanges();
                MainWindow_Loaded(null, e);

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
