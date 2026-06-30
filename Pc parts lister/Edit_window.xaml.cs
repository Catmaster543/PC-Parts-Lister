using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
        public Component Komponenta { get; set; }
        public Edit_window(Component komponenta)
        {
            InitializeComponent();
            Komponenta = komponenta;
            DataContext = Komponenta;

            ReloadAllImages();

            if (komponenta.Status != null)
            {
                StatusBox.SelectedItem = komponenta.Status;
            }

            if (komponenta.Type != null)
            {
                Type_Box.SelectedItem = komponenta.Type;
            }

            if (komponenta.FullImagePath  != null)
            {
                EditMainPic_Button.Visibility = Visibility.Visible;
            }

            Type_Box.ItemsSource = new[]
            {
                "CPU",
                "GPU",
                "RAM",
                "Mb",
                "Disk",
                "PSU",
                "Case",
                "Jiné"
            };

            StatusBox.ItemsSource = new[]
            {
                "Funkční",
                "Ok",
                "Kritický",
                "Nefunkční",
                "Opravený"
            };

            if (Komponenta.Type == "CPU")
            {
                SubType_Panel.Visibility = Visibility.Collapsed;
                Grid.SetColumn(Series_Panel, 0);
                Series_Panel.Margin = new Thickness(0);
                SubSeries_Panel.Visibility = Visibility.Visible;
            }
            else if (Komponenta.Type == "Disk")
            {
                Etc_Grid.Visibility = Visibility.Visible;
                Capacity_Panel.Visibility = Visibility.Visible;
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
                Etc_Grid.Visibility = Visibility.Visible;
                Capacity_Panel.Visibility = Visibility.Visible;
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
                Etc_Grid.Visibility= Visibility.Visible;
                Power_Panel.Visibility = Visibility.Visible;
            }
            else if (komponenta.Type == "Case")
            {
                SerText.Visibility = Visibility.Collapsed;
                SerBox.Visibility = Visibility.Collapsed;
                SubSerText.Visibility = Visibility.Collapsed;
                SubSerBox.Visibility = Visibility.Collapsed;
            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
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
            EditMainPic();
        }

        private void EditMainPic_Click(object sender, RoutedEventArgs e)
        {
            EditMainPic();
        }

        void EditMainPic()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Obrázky (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";

            if (dialog.ShowDialog() == true)
            {
                string imagesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");

                Directory.CreateDirectory(imagesFolder);

                string fileName = Path.GetFileName(dialog.FileName);

                // Aby se nepřepisovaly soubory se stejným názvem
                string uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);

                string newPath = Path.Combine(imagesFolder, uniqueName);

                File.Copy(dialog.FileName, newPath, true);

                // Uložíme RELATIVNÍ cestu
                Komponenta.ImagePath = Path.Combine("Images", uniqueName);
            }
            Main_pic.Source = new BitmapImage(new Uri(Komponenta.FullImagePath, UriKind.Absolute));
        }

        #region Cpu parameters
        private void SubSerBox_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            ModelText.Visibility = Visibility.Visible;
            ModelBox.Visibility = Visibility.Visible;
        }
        #endregion

        #region Power section

        bool powerIsValid = true;
        int powerErrorInt = -1;
        private void Power_TextChanged(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(PowerBox.Text, out int pwCislo))
            {
                if (powerErrorInt != -1)
                {
                    ClearAnError(ref powerErrorInt);
                }
                powerIsValid = true;
                PowerText.Foreground = new SolidColorBrush(Colors.Black);
                PowerBox.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB3ABAB"));
            }
            else if (PowerBox.Text == null || PowerBox.Text == "")
            {
                powerIsValid = true;
                if (powerErrorInt != -1)
                {
                    ClearAnError(ref powerErrorInt);
                }
                PowerText.Foreground = new SolidColorBrush(Colors.Black);
                PowerBox.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB3ABAB"));
            }
            else
            {
                powerIsValid = false;
                if (powerErrorInt == -1)
                {
                    powerErrorInt = WriteAnError($"{PowerBox.Text} není platná hodnota počtu, použijte kladné číslo.", Colors.Red);
                }
                else if (powerErrorInt != -1)
                {
                    AlterAnError(powerErrorInt, $"{PowerBox.Text} není platná hodnota počtu, použijte kladné číslo.");
                }
                PowerText.Foreground = new SolidColorBrush(Colors.Red);
                PowerBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }
        #endregion

        #region Count region
        bool countIsValid = true;
        int countErrorInt = -1;
        private void Count_TextChanged(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(CountBox.Text, out int ctCislo))
            {
                if (countErrorInt != -1)
                {
                    ClearAnError(ref countErrorInt);
                }
                countIsValid = true;
                CountText.Foreground = new SolidColorBrush(Colors.Black);
                CountBox.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB3ABAB"));
            }
            else if (CountBox.Text == null || CountBox.Text == "")
            {
                if (countErrorInt != -1)
                {
                    ClearAnError(ref countErrorInt);
                }
                countIsValid = true;
                CountText.Foreground = new SolidColorBrush(Colors.Black);
                CountBox.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB3ABAB"));
            }
            else
            {
                countIsValid = false;
                if (countErrorInt == -1)
                {
                    countErrorInt = WriteAnError($"{CountBox.Text} není platná hodnota počtu, použijte kladné číslo.", Colors.Red);
                }
                else if (countErrorInt != -1)
                {
                    AlterAnError(countErrorInt, $"{CountBox.Text} není platná hodnota počtu, použijte kladné číslo.");
                }
                CountText.Foreground = new SolidColorBrush(Colors.Red);
                CountBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }
        #endregion

        #region Capacity section

        bool capacityIsValid = true;
        int capacityErrorInt = -1;
        private void Capacity_TextChanged(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(CapacityBox.Text, out int cpCislo))
            {
                if (capacityErrorInt != -1)
                {
                    ClearAnError(ref capacityErrorInt);
                }
                capacityIsValid = true;
                CapacityText.Foreground = new SolidColorBrush(Colors.Black);
                CapacityBox.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB3ABAB"));
            }
            else if (CapacityBox.Text == null || CapacityBox.Text == "")
            {
                capacityIsValid = true;
                if (capacityErrorInt != -1)
                {
                    ClearAnError(ref capacityErrorInt);
                }
                CapacityText.Foreground = new SolidColorBrush(Colors.Black);
                CapacityBox.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB3ABAB"));
            }
            else
            {
                capacityIsValid = false;
                if (capacityErrorInt == -1)
                {
                    capacityErrorInt = WriteAnError($"{CapacityBox.Text} není platná hodnota místa v GB, použijte kladné číslo.", Colors.Red);
                }
                else if (capacityErrorInt != -1)
                {
                    AlterAnError(capacityErrorInt, $"{CapacityBox.Text} není platná hodnota místa v GB, použijte kladné číslo.");
                }
                CapacityText.Foreground = new SolidColorBrush(Colors.Red);
                CapacityBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }
        #endregion

        int currentErrorId = 0;
        int WriteAnError(string message, Color color)
        {
            int tempErrorId = currentErrorId;
            TextBlock errorTextBlock = new TextBlock();
            ErrorPanel.Children.Add(errorTextBlock);
            errorTextBlock.Name = $"ErrorText{tempErrorId}";
            errorTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
            errorTextBlock.FontWeight = FontWeights.Bold;
            errorTextBlock.TextWrapping = TextWrapping.Wrap;
            errorTextBlock.Text = message;
            errorTextBlock.Foreground = new SolidColorBrush(color);
            CalculateIndex();
            Save_Button.IsEnabled = false;
            return tempErrorId;
        }

        bool AlterAnError(int errorId, string messageAlt = null)
        {
            TextBlock Error = (TextBlock)ErrorPanel.Children[errorId];
            if (messageAlt != null)
            {
                Error.Text = messageAlt;
                return true;
            }
            return false;
        }

        bool ClearAnError(ref int errorId)
        {
            if (errorId < powerErrorInt)
            {
                powerErrorInt--;
            }
            if (errorId < countErrorInt)
            {
                countErrorInt--;
            }
            if (errorId < capacityErrorInt)
            {
                capacityErrorInt--;
            }
            ErrorPanel.Children.RemoveAt(errorId);
            errorId = -1;
            CalculateIndex();
            if (ErrorPanel.Children.Count == 0)
            {
                Save_Button.IsEnabled = true;
            }
            return true;
        }

        void CalculateIndex()
        {
            for (int i = 0; i < ErrorPanel.Children.Count + 1; i++)
            {
                if (i == ErrorPanel.Children.Count)
                {
                    currentErrorId = i;
                }
                else if (ErrorPanel.Children[i] == null)
                {
                    currentErrorId = i;
                }
            }
        }

        void AddImage_Click(object sender, RoutedEventArgs e)
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
                Komponenta.imagePaths.Add(Path.Combine("Images", uniqueName));
            }
            ReloadAllImages();
        }

        void ReloadAllImages()
        {
            for (int i = 0; i < ImagesPanel.Children.Count; i++)
            {
                var imagesToRemove = ImagesPanel.Children.OfType<Grid>().ToList();
                foreach (Grid grid in imagesToRemove)
                {
                    ImagesPanel.Children.Remove(grid);
                }
                }
            if (Komponenta.imagePaths != null)
            {
                for (int i = 0; i < Komponenta.imagePaths.Count; i++)
                {
                    if (Komponenta.imagePaths[i] != null && File.Exists(Komponenta.imagePaths[i]))
                    {
                        ConstructAnImageFrame(Komponenta.imagePaths[i]);
                        /*
                        Image image = new Image();
                        string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,Komponenta.imagePaths[i]);
                        image.Source = new BitmapImage(new Uri(fullPath, UriKind.Absolute));
                        image.MaxHeight = 60;
                        image.MaxWidth = 60;
                        image.Stretch = Stretch.UniformToFill;
                        ImagesPanel.Children.Insert(ImagesPanel.Children.Count -1, image);
                        */
                    }
                    else if (!File.Exists(Komponenta.imagePaths[i]))
                    {
                        Komponenta.imagePaths.Remove(Komponenta.imagePaths[i]);
                    }
                }
            }
        }

        void ConstructAnImageFrame(string imagePath)
        {
            Grid grid = new Grid();
            Button deleteButton = new Button();
            Button imageButton = new Button();

            ImagesPanel.Children.Insert(ImagesPanel.Children.Count -1, grid);
            grid.Margin = new Thickness(7);
            grid.Width = 60;
            grid.Height = 60;

            ImageBrush imageBrush = new ImageBrush(LoadImage(imagePath)); 
            imageBrush.Stretch = Stretch.Uniform;
            imageButton.Background = imageBrush;
            imageButton.Width = 60;
            imageButton.Height = 60;
            imageButton.Tag = imagePath;
            imageButton.Click += OpenImage_Click;
            grid.Children.Add(imageButton);

            deleteButton.Margin = new Thickness(1);
            deleteButton.BorderThickness = new Thickness(0);
            deleteButton.Width = 10;
            deleteButton.Height = 10;
            deleteButton.HorizontalAlignment = HorizontalAlignment.Left;
            deleteButton.VerticalAlignment = VerticalAlignment.Top;
            deleteButton.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/cancel_button.png")));
            deleteButton.Click += DeleteImage_Click;
            grid.Children.Add(deleteButton);
        }

        private void DeleteImage_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Grid grid = button.Parent as Grid;
            Button imageButton = (Button)grid.Children[0];
            foreach (string path in Komponenta.imagePaths)
            {
                if (path == imageButton.Tag.ToString())
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    Komponenta.imagePaths.Remove(path);
                    break;
                }
            }
            ImagesPanel.Children.Remove(grid);
        }

        private void OpenImage_Click(Object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            
            Photo_View Photo_Page = new Photo_View(button.Tag.ToString());
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
    }
}
