using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

            if (komponenta.imagePaths.Count > 0)
            {
                Null_Photos_TextBlock.Visibility = Visibility.Collapsed;
            }

            if (komponenta.FullImagePath != null)
            {
                Null_Main_Pic_TextBlock.Visibility = Visibility.Collapsed;
            }

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

            MainPicButton.Click += OpenImage_Click;

            if (Komponenta.imagePaths != null)
            {
                for (int i = 0; i < Komponenta.imagePaths.Count; i++)
                {
                    if (Komponenta.imagePaths[i] != null && File.Exists(Komponenta.imagePaths[i]))
                    {
                        ConstructAnImageFrame(Komponenta.imagePaths[i]);
                    }
                    else if (!File.Exists(Komponenta.imagePaths[i]))
                    {
                        Komponenta.imagePaths.Remove(Komponenta.imagePaths[i]);
                    }
                }
            }
        }



        // Původ z Edit window
        void ConstructAnImageFrame(string imagePath)
        {
            Grid grid = new Grid();
            Button deleteButton = new Button();
            Button imageButton = new Button();

            ImagesPanel.Children.Insert(ImagesPanel.Children.Count - 1, grid);
            grid.Margin = new Thickness(7);
            grid.Width = 60;
            grid.Height = 60;

            if (File.Exists(imagePath))
            {
                ImageBrush imageBrush = new ImageBrush(LoadImage(imagePath));
                imageBrush.Stretch = Stretch.Uniform;
                imageButton.Background = imageBrush;
            }
            else if (!File.Exists(imagePath))
            {
                return;
            }
            imageButton.Width = 60;
            imageButton.Height = 60;
            imageButton.BorderThickness = new Thickness(0);
            imageButton.Tag = imagePath;
            imageButton.Click += OpenImage_Click;
            grid.Children.Add(imageButton);
        }

        private void OpenImage_Click(Object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            Photo_View Photo_Page = new Photo_View(button.Tag.ToString(), Komponenta);
            Photo_Page.ShowDialog();
        }

        BitmapImage LoadImage(string path)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.UriSource = new Uri(path, UriKind.Relative);
            bitmap.EndInit();
            bitmap.Freeze();

            return bitmap;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
