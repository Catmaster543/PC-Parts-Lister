using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
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
    public partial class Edit_window : Window
    {
        public Component Komponenta { get; }
        public Edit_window(Component komponenta)
        {
            InitializeComponent();
            Komponenta = komponenta;
            DataContext = Komponenta;

            if (komponenta.Status != null)
            {
                StatusBox.SelectedItem = komponenta.Status;
            }

            StatusBox.ItemsSource = new[]
            {
                "Funkční",
                "Ok",
                "Kritický",
                "Nefunkční",
                "Opravený"
            };

            #region Hiding
            PowerText.Visibility = Visibility.Collapsed;
            PowerBox.Visibility = Visibility.Collapsed;
            CapacityText.Visibility = Visibility.Collapsed;
            CapacityBox.Visibility = Visibility.Collapsed;
            #endregion

            if (Komponenta.Type == "CPU")
            {
                TypeText2.Visibility = Visibility.Collapsed;
                TypeBox2.Visibility = Visibility.Collapsed;
            }
            else if (Komponenta.Type == "Disk")
            {
                CapacityText.Visibility = Visibility.Visible;
                CapacityBox.Visibility = Visibility.Visible;
                SerText.Visibility = Visibility.Collapsed;
                SerBox.Visibility = Visibility.Collapsed;
                SubSerText.Visibility = Visibility.Collapsed;
                SubSerBox.Visibility = Visibility.Collapsed;
                TypeText2.Text = "Typ disku:";
            }
            else if (Komponenta.Type == "GPU")
            {
                SubSerText.Visibility = Visibility.Collapsed;
                SubSerBox.Visibility = Visibility.Collapsed;
                TypeText2.Text = "Výrobce čipu:";
            }
            else if (komponenta.Type == "RAM")
            {
                SubSerText.Visibility = Visibility.Collapsed;
                SubSerBox.Visibility = Visibility.Collapsed;
            }
            else if (komponenta.Type == "Mb")
            {
                SerText.Visibility = Visibility.Collapsed;
                SerBox.Visibility = Visibility.Collapsed;
                SubSerText.Visibility = Visibility.Collapsed;
                SubSerBox.Visibility = Visibility.Collapsed;
                TypeText2.Text = "Pro jaké procesory?";
            }
            else if (komponenta.Type == "PSU")
            {
                TypeText2.Visibility = Visibility.Collapsed;
                TypeBox2.Visibility = Visibility.Collapsed;
                SerText.Visibility = Visibility.Collapsed;
                SerBox.Visibility = Visibility.Collapsed;
                SubSerText.Visibility = Visibility.Collapsed;
                SubSerBox.Visibility = Visibility.Collapsed;
                PowerText.Visibility = Visibility.Visible;
                PowerBox.Visibility = Visibility.Visible;
            }
            else if (komponenta.Type == "Case")
            {
                SerText.Visibility = Visibility.Collapsed;
                SerBox.Visibility = Visibility.Collapsed;
                SubSerText.Visibility = Visibility.Collapsed;
                SubSerBox.Visibility = Visibility.Collapsed;
            }
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (StatusBox.SelectedItem != null)
            {
                Komponenta.Status = StatusBox.SelectedItem.ToString();
            }
            Close();
        }

        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Obrázky (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";

            if (dialog.ShowDialog() == true)
            {
                string imagesFolder = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Images");

                Directory.CreateDirectory(imagesFolder);

                string fileName = Path.GetFileName(dialog.FileName);

                // Aby se nepřepisovaly soubory se stejným názvem
                string uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);

                string newPath = Path.Combine(imagesFolder, uniqueName);

                File.Copy(dialog.FileName, newPath, true);

                // Uložíme RELATIVNÍ cestu
                Komponenta.ImagePath = Path.Combine("Images", uniqueName);
            }
        }
    }
}
