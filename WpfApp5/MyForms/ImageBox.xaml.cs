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
    /// Логика взаимодействия для ImageBox.xaml
    /// </summary>
    public partial class ImageBox : Window
    {

        /// <summary>
        /// выбранная  картинка  
        /// </summary>
        internal ModelView.ModelImage SelectImage { get; set; } = new ModelView.ModelImage();      
        public ImageBox()
        {
            InitializeComponent();
            this.Loaded += ImageBox_Loaded;
        }

        private void ImageBox_Loaded(object sender, RoutedEventArgs e)
        {
            listImage.ItemsSource = Controllers.ImageController.GetImage(); // получаем  список  картинок  из  контроллера  в листбокс 
        }

        /// <summary>
        /// двойное  нажатие на  картинку 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listImage_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var s =(ModelView.ModelImage)listImage.SelectedItem; // запоминаем  выбранную  картинку  из  листа 
            if (s != null) // если не пусто 
                SelectImage = s; // запишем ее  в  поле 
            DialogResult = true; // закроем  окно с результатом  true 
        }
    }
}
