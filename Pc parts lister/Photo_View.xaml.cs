using System;
using System.Collections.Generic;
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

namespace Pc_parts_lister
{
    /// <summary>
    /// Interakční logika pro Photo_View.xaml
    /// </summary>
    public partial class Photo_View : Window
    {
        public Component Komponenta { get; set; }
        public int currentPhotoIndex;
        public Photo_View(string imagePath, Component komponenta, bool isMainImage = false)
        {
            InitializeComponent();

            Komponenta = komponenta;

            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imagePath);

            mainImage.Source = new BitmapImage(new Uri(fullPath, UriKind.Absolute));

            if (isMainImage)
            {
                Previous_Image_Button.Visibility = Visibility.Collapsed;
                Next_Image_Button.Visibility= Visibility.Collapsed;
            }
            else
            {
                if (komponenta.imagePaths.Count > 1)
                {
                    for (int i = 0; Komponenta.imagePaths.Count < 0; i++)
                    {
                        if (imagePath == komponenta.imagePaths[i])
                        {
                            currentPhotoIndex = i;
                        }
                    }
                }
                else
                {
                    Previous_Image_Button.Visibility = Visibility.Collapsed;
                    Next_Image_Button.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void SwitchImage(int amount = 1, bool add = true)
        {
            string smolPath = "";
            if (add == true)
            {
                if (currentPhotoIndex + amount <= Komponenta.imagePaths.Count - 1)
                {
                    smolPath = Komponenta.imagePaths[currentPhotoIndex + amount];
                    currentPhotoIndex += amount;
                }
                else
                {
                    smolPath = Komponenta.imagePaths.First();
                    currentPhotoIndex = 0;
                }
            }
            else if (add == false)
            {
                if (currentPhotoIndex - amount >= 0)
                {
                    smolPath = Komponenta.imagePaths[currentPhotoIndex - amount];
                    currentPhotoIndex -= amount;
                }
                else
                {
                    smolPath = Komponenta.imagePaths.Last();
                    currentPhotoIndex = Komponenta.imagePaths.Count - 1;
                }
            }

            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, smolPath);
            mainImage.Source = new BitmapImage(new Uri(fullPath));
        }

        private void NextImage_Click(object sender, RoutedEventArgs e)
        {
            SwitchImage(1, true);
        }

        private void PreviousImage_Click(Object sender, RoutedEventArgs e)
        {
            SwitchImage(1, false);
        }
    }
}
