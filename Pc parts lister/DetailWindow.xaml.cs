using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

namespace Pc_parts_lister
{
    public partial class DetailWindow : Window
    {
        public Component Komponenta { get; }

        public DetailWindow(Component komponenta)
        {
            InitializeComponent();

            Komponenta = komponenta;
            DataContext = Komponenta;

            #region Hiding
            PowerBox.Visibility = Visibility.Collapsed;
            CapacityBox.Visibility = Visibility.Collapsed;
            #endregion

            if (Komponenta.Type == "CPU")
            {
                TypeBox2.Visibility = Visibility.Collapsed;
            }
            else if (Komponenta.Type == "Disk")
            {
                CapacityBox.Visibility = Visibility.Visible;
                SerBox.Visibility = Visibility.Collapsed;
                SubSerBox.Visibility = Visibility.Collapsed;
            }
            else if (Komponenta.Type == "GPU")
            {
                SubSerBox.Visibility = Visibility.Collapsed;
            }
            else if (komponenta.Type == "RAM")
            {
                SubSerBox.Visibility = Visibility.Collapsed;
            }
            else if (komponenta.Type == "Mb")
            {
                SerBox.Visibility = Visibility.Collapsed;
                SubSerBox.Visibility = Visibility.Collapsed;
            }
            else if (komponenta.Type == "PSU")
            {
                TypeBox2.Visibility = Visibility.Collapsed;
                SerBox.Visibility = Visibility.Collapsed;
                SubSerBox.Visibility = Visibility.Collapsed;
                PowerBox.Visibility = Visibility.Visible;
            }
            else if (komponenta.Type == "Case")
            {
                SerBox.Visibility = Visibility.Collapsed;
                SubSerBox.Visibility = Visibility.Collapsed;
            }
        }
        
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
