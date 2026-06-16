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

            if (komponenta.Type != null)
            {
                Type_Box.SelectedItem = komponenta.Type;
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
    }
}
