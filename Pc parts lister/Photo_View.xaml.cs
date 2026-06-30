using System;
using System.Collections.Generic;
using System.IO;
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

namespace Pc_parts_lister
{
    /// <summary>
    /// Interakční logika pro Photo_View.xaml
    /// </summary>
    public partial class Photo_View : Window
    {
        public Photo_View(string imagePath)
        {
            InitializeComponent();

            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imagePath);

            mainImage.Source = new BitmapImage(new Uri(fullPath, UriKind.Absolute));
        }
    }
}
