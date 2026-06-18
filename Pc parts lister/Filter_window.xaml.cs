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
    /// <summary>
    /// Interakční logika pro Filter_window.xaml
    /// </summary>
    public partial class Filter_window : Window
    {
        public Lookup_window.SearchFilters Filters { get; }
        public Filter_window(Lookup_window.SearchFilters filters)
        {
            InitializeComponent();

            Filters = filters;
            DataContext = Filters;

            #region Hiding
            InitializeComponent();
            SerText.Visibility = Visibility.Collapsed;
            SerBox.Visibility = Visibility.Collapsed;
            SubSerText.Visibility = Visibility.Collapsed;
            SubSerBox.Visibility = Visibility.Collapsed;
            ModelText.Visibility = Visibility.Collapsed;
            ModelBox.Visibility = Visibility.Collapsed;
            Powertext.Visibility = Visibility.Collapsed;
            PowerGrid.Visibility = Visibility.Collapsed;
            CapacityText.Visibility = Visibility.Collapsed;
            CapacityGrid.Visibility = Visibility.Collapsed;
            TypeText2.Visibility = Visibility.Collapsed;
            TypeBox2.Visibility = Visibility.Collapsed;
            #endregion

            TypeBox.ItemsSource = new[]
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
        }

        public Component Component { get; private set; }

        private void TypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var type = TypeBox.SelectedItem?.ToString();
            Manufacturertext.Visibility = Visibility.Visible;
            ManufacturerBox.Visibility = Visibility.Visible;


            #region Cpu options 
            if (type == "CPU")
            {
                ManufacturerBox.IsEditable = false;
                ManufacturerBox.ItemsSource = new[]
                {
                    "Intel",
                    "AMD",
                    "Qualcomm"
                };
            }
            #endregion

            #region Disk options
            else if (type == "Disk")
            {
                #region Visibility
                TypeText2.Visibility = Visibility.Visible;
                TypeBox2.Visibility = Visibility.Visible;
                CapacityText.Visibility = Visibility.Visible;
                CapacityGrid.Visibility = Visibility.Visible;
                ModelText.Visibility = Visibility.Visible;
                ModelBox.Visibility = Visibility.Visible;
                #endregion
                TypeText2.Text = "Typ disku:";
                TypeBox2.ItemsSource = new[]
                {
                    "SSD",
                    "HDD",
                    "SSHD"
                };
            }
            #endregion

            #region Gpu options 
            else if (type == "GPU")
            {
                #region Visibility
                TypeText2.Visibility = Visibility.Visible;
                TypeBox2.Visibility = Visibility.Visible;
                #endregion
                TypeText2.Text = "Výrobce čipu:";
                TypeBox2.IsEditable = true;
                TypeBox2.ItemsSource = new[]
                {
                    "Nvidia",
                    "AMD",
                    "Intel",
                    "Ati"
                };
            }
            #endregion

            #region RAM options
            else if (type == "RAM")
            {
                #region Visibility
                TypeText2.Visibility = Visibility.Visible;
                TypeBox2.Visibility = Visibility.Visible;
                CapacityText.Visibility = Visibility.Visible;
                CapacityGrid.Visibility = Visibility.Visible;
                SerText.Visibility = Visibility.Visible;
                SerBox.Visibility = Visibility.Visible;
                ModelText.Visibility = Visibility.Visible;
                ModelBox.Visibility = Visibility.Visible;
                #endregion

                ManufacturerBox.ItemsSource = new[]
                {
                    "Kingston",
                    "Corsair",
                    "XPG",
                    "HP",
                    "Dell",
                    "Patriot",
                    "G.Skill",
                    "Crucial",
                    "Apacer"
                };

                SerBox.ItemsSource = new[]
                {
                    "DDR5",
                    "DDR4",
                    "DDR3",
                    "DDR2",
                    "DDR"
                };

                TypeBox2.ItemsSource = new[]
                {
                    "DIMM",
                    "SODIMM"
                };
            }
            #endregion

            #region Mb options 
            else if (type == "Mb")
            {
                #region Visibility
                TypeText2.Visibility = Visibility.Visible;
                TypeBox2.Visibility = Visibility.Visible;
                ModelText.Visibility = Visibility.Visible;
                ModelBox.Visibility = Visibility.Visible;
                #endregion

                TypeText2.Text = "Pro jaké procesory?";
                TypeBox2.ItemsSource = new[]
                {
                    "Intel",
                    "AMD"
                };

                ManufacturerBox.ItemsSource = new[]
                {
                    "Gigabyte",
                    "Asus",
                    "Asrock",
                    "MSI",
                    "HP",
                    "Dell"
                };
            }
            #endregion

            #region PSU options
            else if (type == "PSU")
            {
                #region Visibility
                ModelText.Visibility = Visibility.Visible;
                ModelBox.Visibility = Visibility.Visible;
                Powertext.Visibility = Visibility.Visible;
                PowerGrid.Visibility = Visibility.Visible;
                #endregion


                ManufacturerBox.ItemsSource = new[]
                {
                    "Gigabyte",
                    "Asus",
                    "Akasa",
                    "MSI",
                    "Corsair",
                    "ADATA",
                    "Be quiet!",
                    "Evolveo",
                    "Cooler master",
                    "Endorfy",
                    "Fortron",
                    "Deepcool",
                    "Eurocase",
                    "Fujitsu",
                    "Montech",
                    "Zalman",
                    "Thermaltake",
                    "HP",
                    "Dell"
                };
            }
            #endregion

            #region Case options
            else if (type == "Case")
            {
                #region Visibility
                TypeText2.Visibility = Visibility.Visible;
                TypeBox2.Visibility = Visibility.Visible;
                ModelText.Visibility = Visibility.Visible;
                ModelBox.Visibility = Visibility.Visible;
                #endregion

                TypeBox2.IsEditable = false;
                TypeBox2.ItemsSource = new[]
                {
                    "E-ATX",
                    "ATX",
                    "M-ATX",
                    "Micro-ATX",
                    "Custom"
                };

                ManufacturerBox.ItemsSource = new[]
                {
                    "1stcool",
                    "Fracture",
                    "Lenovo",
                    "Gamemax",
                    "HP",
                    "Dell"
                };
            }
            #endregion

            #region Other options
            else
            {
                #region Visibility
                Manufacturertext.Visibility = Visibility.Visible;
                ManufacturerBox.Visibility = Visibility.Visible;
                ModelText.Visibility = Visibility.Visible;
                ModelBox.Visibility = Visibility.Visible;
                #endregion

                ManufacturerBox.ItemsSource = null;
            }
            #endregion
        }

        private void TypeBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var Type2 = TypeBox2.SelectedItem?.ToString();

            #region Disk options 
            if (TypeBox.Text == "Disk")
            {
                ManufacturerBox.ItemsSource = new[]
                {
                    "Seagate",
                    "WD",
                    "Sandisk",
                    "Patriot",
                    "Kingston",
                    "Verbatim",
                    "Corsair",
                    "XPG"
                };
            }
            #endregion

            #region GPU options
            else if (TypeBox.Text == "GPU")
            {
                ModelText.Visibility = Visibility.Visible;
                ModelBox.Visibility = Visibility.Visible;
                if (Type2 == "Nvidia")
                {
                    SerText.Visibility = Visibility.Visible;
                    SerBox.Visibility = Visibility.Visible;
                    ManufacturerBox.ItemsSource = new[]
                    {
                        "Asus",
                        "Asrock",
                        "EVGA",
                        "Gigabyte",
                        "Gainward",
                        "MSI",
                        "Inno3d",
                        "PNY",
                        "PALIT",
                        "Zotac"
                    };
                }
                else if (Type2 == "AMD")
                {
                    SerText.Visibility = Visibility.Collapsed;
                    SerBox.Visibility = Visibility.Collapsed;
                    ManufacturerBox.ItemsSource = new[]
                    {
                        "Asus",
                        "Asrock",
                        "Gigabyte",
                        "XFX",
                        "Sapphire",
                        "Acer"
                    };
                }
                else if (Type2 == "Intel")
                {
                    SerText.Visibility = Visibility.Collapsed;
                    SerBox.Visibility = Visibility.Collapsed;
                    ManufacturerBox.ItemsSource = new[]
                    {
                        "Intel",
                        "Sparkle",
                        "Acer",
                        "Asrock"
                    };
                }
                else
                {
                    SerText.Visibility = Visibility.Collapsed;
                    SerBox.Visibility = Visibility.Collapsed;
                    ManufacturerBox.ItemsSource = null;
                }
            }
            #endregion

        }

        private void ManufacturerBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var manufacturer = ManufacturerBox.SelectedItem?.ToString();

            #region Cpu options
            if (TypeBox.Text == "CPU")
            {
                SerText.Visibility = Visibility.Visible;
                SerBox.Visibility = Visibility.Visible;
                if (manufacturer == "Intel")
                {
                    SerBox.ItemsSource = new[]
                    {
                        "Core",
                        "Xeon",
                        "Atom",
                        "Pentium",
                        "Celeron"
                    };
                }
                else if (manufacturer == "AMD")
                {
                    SerBox.ItemsSource = new[]
                    {
                        "Ryzen",
                        "Threadripper",
                        "Athlon"
                    };
                }
                else if (manufacturer == "Qualcomm")
                {
                    ModelText.Visibility = Visibility.Visible;
                    ModelBox.Visibility = Visibility.Visible;
                    SerBox.ItemsSource = null;
                }
            }
            #endregion

            #region Gpu options
            if (TypeBox.Text == "GPU")
            {
                if (TypeBox2.Text == "Nvidia")
                {
                    SerBox.ItemsSource = new[]
                    {
                        "RTX", "GTX", "GT", "A"
                    };
                }
                else
                {
                    SerBox.ItemsSource = null;
                }
            }
            #endregion
        }

        private void SerBox_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            var Series = SerBox.SelectedItem?.ToString();

            #region Cpu options
            if (TypeBox.Text == "CPU")
            {
                if (Series == "Core")
                {
                    SubSerText.Visibility = Visibility.Visible;
                    SubSerBox.Visibility = Visibility.Visible;

                    SubSerBox.ItemsSource = new[]
                    {
                        "i3",
                        "i5",
                        "i7",
                        "i9",
                        "3",
                        "5",
                        "7",
                        "Ultra 5",
                        "Ultra 7",
                        "Ultra 9"
                    };
                }
                else if (Series == "Ryzen")
                {
                    SubSerText.Visibility = Visibility.Visible;
                    SubSerBox.Visibility = Visibility.Visible;

                    SubSerBox.ItemsSource = new[]
                    {
                        "3",
                        "5",
                        "7",
                        "9"
                    };
                }
                else if (TypeBox.Text == "Cpu" && Series != "Ryzen" && Series != "Core")
                {
                    SubSerText.Visibility = Visibility.Visible;
                    SubSerBox.Visibility = Visibility.Visible;

                    SubSerBox.ItemsSource = null;
                }
                else
                {
                    SubSerBox.ItemsSource = null;
                }
            }
            #endregion
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
        int powerCompErrorInt;
        int powerErrorInt;
        private void Power_TextChanged(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(PowerBox.Text, out int pwCislo))
            {
                if (Filters.powerCompMode == null && powerCompErrorInt == 0)
                {
                    powerCompErrorInt = WriteAnError($"Zvolte prosím režim pro hledání zdroje podle W, jinak bude použit defaultní: \"=\"", Colors.Orange);
                }
                if (powerErrorInt != 0)
                {
                    ClearAnError(ref powerErrorInt);
                }
                powerIsValid = true;
                Filters.power = pwCislo;
                Powertext.Foreground = new SolidColorBrush(Colors.Black);
                PowerBox.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB3ABAB"));
            }
            else if (PowerBox.Text == null || PowerBox.Text == "")
            {
                powerIsValid = true;
                if (powerErrorInt != 0)
                {
                    ClearAnError(ref powerErrorInt, true);
                }
                Powertext.Foreground = new SolidColorBrush(Colors.Black);
                PowerBox.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB3ABAB"));
            }
            else
            {
                powerIsValid = false;
                if (powerErrorInt == 0)
                {
                    powerErrorInt = WriteAnError($"{PowerBox.Text} není platná hodnota počtu, použijte kladné číslo.", Colors.Red, true);
                }
                else if (powerErrorInt != 0)
                {
                    AlterAnError(powerErrorInt, $"{PowerBox.Text} není platná hodnota počtu, použijte kladné číslo.");
                }
                Powertext.Foreground = new SolidColorBrush(Colors.Red);
                PowerBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }
        private void Power_Smaller_Click(object sender, RoutedEventArgs e)
        {
            Filters.powerCompMode = "<";
            if (powerCompErrorInt != 0)
            {
                ClearAnError(ref powerCompErrorInt);
            }
            SmallerPowerComp_Button.BorderBrush = new SolidColorBrush(Colors.GreenYellow);
            BiggerPowerComp_Button.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF707070"));
            EqualPowerComp_Button.BorderBrush= new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF707070"));
        }
        private void Power_Bigger_Click(object sender, RoutedEventArgs e)
        {
            Filters.powerCompMode = ">";
            if (powerCompErrorInt != 0)
            {
                ClearAnError(ref powerCompErrorInt);
            }
            BiggerPowerComp_Button.BorderBrush = new SolidColorBrush(Colors.GreenYellow);
            SmallerPowerComp_Button.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF707070"));
            EqualPowerComp_Button.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF707070"));
        }
        private void Power_Same_Click(object sender, RoutedEventArgs e)
        {
            Filters.powerCompMode = "=";
            if (powerCompErrorInt != 0)
            {
                ClearAnError(ref powerCompErrorInt);
            }
            EqualPowerComp_Button.BorderBrush = new SolidColorBrush(Colors.GreenYellow);
            BiggerPowerComp_Button.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF707070"));
            SmallerPowerComp_Button.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF707070"));
        }
        #endregion

        #region Count region
        bool countIsValid = true;
        int countCompErrorInt;
        int countErrorInt;
        private void Count_TextChanged(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(CountBox.Text, out int ctCislo))
            {
                if (Filters.countCompMode == null && countCompErrorInt == 0)
                {
                    countCompErrorInt = WriteAnError("Zvolte prosím režim pro hledání počtu, jinak bude použit defaultní: \"=\"", Colors.Orange);
                }
                if (countErrorInt != 0)
                {
                    ClearAnError(ref countErrorInt, true);
                }
                countIsValid = true;
                Filters.count = ctCislo;
                CountText.Foreground = new SolidColorBrush(Colors.Black);
                CountBox.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB3ABAB"));
            }
            else if (CountBox.Text == null || CountBox.Text == "")
            {
                if (countErrorInt != 0)
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
                if (countErrorInt == 0)
                {
                    countErrorInt = WriteAnError($"{ CountBox.Text} není platná hodnota počtu, použijte kladné číslo.", Colors.Red, true);
                }
                else if (countErrorInt != 0)
                {
                    AlterAnError(countErrorInt, $"{CountBox.Text} není platná hodnota počtu, použijte kladné číslo.");
                }
                CountText.Foreground = new SolidColorBrush(Colors.Red);
                CountBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }
        private void Count_Smaller_Click(object sender, RoutedEventArgs e)
        {
            Filters.countCompMode = "<";
            if (countCompErrorInt != 0)
            {
                ClearAnError(ref countCompErrorInt);
            }
            SmallerCountComp_Button.BorderBrush = new SolidColorBrush(Colors.GreenYellow);
            BiggerCountComp_Button.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF707070"));
            EqualCountComp_Button.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF707070"));
        }
        private void Count_Bigger_Click(object sender, RoutedEventArgs e)
        {
            Filters.countCompMode = ">";
            if (countCompErrorInt != 0)
            {
                ClearAnError(ref countCompErrorInt);
            }
            BiggerCountComp_Button.BorderBrush = new SolidColorBrush(Colors.GreenYellow);
            SmallerCountComp_Button.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF707070"));
            EqualCountComp_Button.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF707070"));
        }
        private void Count_Same_Click(object sender, RoutedEventArgs e)
        {
            Filters.countCompMode = "=";
            if (countCompErrorInt != 0)
            {
                ClearAnError(ref countCompErrorInt);
            }
            EqualCountComp_Button.BorderBrush = new SolidColorBrush(Colors.GreenYellow);
            BiggerCountComp_Button.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF707070"));
            SmallerCountComp_Button.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF707070"));
        }
        #endregion

        #region Capacity section

        bool capacityIsValid = true;
        int capacityCompErrorInt;
        int capacityErrorInt;
        private void Capacity_TextChanged(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(CapacityBox.Text, out int cpCislo))
            {
                if (Filters.capacityCompMode == null && capacityCompErrorInt == 0)
                {
                    capacityCompErrorInt = WriteAnError($"Zvolte prosím režim pro hledání disků podle množství GB, jinak bude použit defaultní: \"=\"", Colors.Orange);
                }
                if (capacityErrorInt != 0)
                {
                    ClearAnError(ref capacityErrorInt, true);
                }
                capacityIsValid = true;
                Filters.capacity = cpCislo;
                CapacityText.Foreground = new SolidColorBrush(Colors.Black);
                CapacityBox.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB3ABAB"));
            }
            else if (CapacityBox.Text == null || CapacityBox.Text == "")
            {
                capacityIsValid = true;
                if (capacityErrorInt != 0)
                {
                    ClearAnError(ref capacityErrorInt, true);
                }
                CapacityText.Foreground = new SolidColorBrush(Colors.Black);
                CapacityBox.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB3ABAB"));
            }
            else
            {
                capacityIsValid = false;
                if (capacityErrorInt == 0)
                {
                    capacityErrorInt = WriteAnError($"{CapacityBox.Text} není platná hodnota místa v GB, použijte kladné číslo.", Colors.Red, true);
                }
                else if (capacityErrorInt != 0)
                {
                    AlterAnError(capacityErrorInt, $"{CapacityBox.Text} není platná hodnota místa v GB, použijte kladné číslo.");
                }
                CapacityText.Foreground = new SolidColorBrush(Colors.Red);
                CapacityBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }
        private void Capacity_Smaller_Click(object sender, RoutedEventArgs e)
        {
            Filters.capacityCompMode = "<";
            if (capacityCompErrorInt != 0)
            {
                ClearAnError(ref capacityCompErrorInt);
            }
            SmallerCapacityComp_Button.BorderBrush = new SolidColorBrush(Colors.GreenYellow);
            BiggerCapacityComp_Button.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF707070"));
            EqualCapacityComp_Button.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF707070"));
        }
        private void Capacity_Bigger_Click(object sender, RoutedEventArgs e)
        {
            Filters.capacityCompMode = ">";
            if (capacityCompErrorInt != 0)
            {
                ClearAnError(ref capacityCompErrorInt);
            }
            BiggerCapacityComp_Button.BorderBrush = new SolidColorBrush(Colors.GreenYellow);
            SmallerCapacityComp_Button.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF707070"));
            EqualCapacityComp_Button.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF707070"));
        }
        private void Capacity_Same_Click(object sender, RoutedEventArgs e)
        {
            Filters.capacityCompMode = "=";
            if (capacityCompErrorInt != 0)
            {
                ClearAnError(ref capacityCompErrorInt);
            }
            EqualCapacityComp_Button.BorderBrush = new SolidColorBrush(Colors.GreenYellow);
            BiggerCapacityComp_Button.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF707070"));
            SmallerCapacityComp_Button.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF707070"));
        }
        #endregion

        int currentErrorId = 1;
        int critErrors = 0;
        int WriteAnError(string message, Color color, bool IsCritical = false)
        {
            int tempErrorId = currentErrorId;
            if (IsCritical)
            {
                critErrors++;
                Save_Button.IsEnabled = false;
            }
            TextBlock errorTextBlock = new TextBlock();
            SaveNErrorPanel.Children.Add(errorTextBlock);
            errorTextBlock.Name = $"ErrorText{tempErrorId}";
            errorTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
            errorTextBlock.FontWeight = FontWeights.Bold;
            errorTextBlock.TextWrapping = TextWrapping.Wrap;
            errorTextBlock.Text = message;
            errorTextBlock.Foreground = new SolidColorBrush(color);
            CalculateIndex();
            return tempErrorId;
        }

        bool AlterAnError(int errorId, string messageAlt = null)
        {
            TextBlock Error = (TextBlock)SaveNErrorPanel.Children[errorId];
            if (messageAlt != null)
            {
                Error.Text = messageAlt;
                return true;
            }
            return false;
        }

        bool ClearAnError(ref int errorId, bool IsCritical = false)
        {
            if (errorId < powerErrorInt)
            {
                powerErrorInt--;
            }
            if (errorId < powerCompErrorInt)
            {
                powerCompErrorInt--;
            }
            if (errorId < countErrorInt)
            {
                countErrorInt--;
            }
            if (errorId < countCompErrorInt)
            {
                countCompErrorInt--;
            }
            if (errorId < capacityErrorInt)
            {
                capacityErrorInt--;
            }
            if (errorId < capacityCompErrorInt)
            {
                capacityCompErrorInt--;
            }
            SaveNErrorPanel.Children.RemoveAt(errorId);
            errorId = 0;
            CalculateIndex();
            if (IsCritical)
            {
                critErrors--;   
            }
            if (critErrors == 0)
            {
                Save_Button.IsEnabled = true;
            }
            return true;
        }

        void CalculateIndex()
        {
            for (int i = 0; i < SaveNErrorPanel.Children.Count + 1; i++)
            {
                if (i == SaveNErrorPanel.Children.Count)
                {
                    currentErrorId = i;
                }
                else if (SaveNErrorPanel.Children[i] == null)
                {
                    currentErrorId = i;
                }
            }
        }

        private void Favorite_ButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Filters.filterFavorite = true;
            UnFilterFavorite_Button.Visibility = Visibility.Visible;
            Favorite_Button.BorderBrush = new SolidColorBrush(Colors.Green);
            if (Filters.favorite)
            {
                Filters.favorite = false;
            }
            else if (!Filters.favorite)
            {
                Filters.favorite = true;
            }
            ChangeButtonBg(button, Filters.favoriteIconPath);
        }

        private void UnFilterFavorite_Click(object sender, RoutedEventArgs e)
        {
            Filters.filterFavorite = false;
            Filters.favorite = false;
            Favorite_Button.BorderBrush = null;
            UnFilterFavorite_Button.Visibility = Visibility.Collapsed;
            ChangeButtonBg(Favorite_Button, Filters.favoriteIconPath);
        }

        private void Save_Filter_Click(object sender, RoutedEventArgs e)
        {
            if (TypeBox.Text == "CPU")
            {
                Filters.type = TypeBox.SelectedItem.ToString();
                Filters.manufacturer = ManufacturerBox.SelectedItem.ToString();
            }
            else if (TypeBox.SelectedItem == null)
            {
                Filters.type = null;
            }
            else
            {
                Filters.type = TypeBox.SelectedItem.ToString();
            }

            if (ManufacturerBox.Text != null && ManufacturerBox.Text != "Vyber, nebo napiš vlastní")
            {
                Filters.manufacturer = ManufacturerBox.Text;
            }

            if (StatusBox.SelectedItem != null)
            {
                Filters.status = StatusBox.SelectedItem.ToString();
            }

            if (SerBox.Text != null && SerBox.Text != "Vyber, nebo napiš vlastní")
            {
                Filters.series = SerBox.Text;
            }

            if (TypeBox2.SelectedItem != null)
            {
                Filters.type2 = TypeBox2.SelectedItem.ToString();
            }

            if (ModelBox.Text != null || ModelBox.Text == "")
            {
                Filters.model = ModelBox.Text;
            }

            if (Filters.powerCompMode == null)
            {
                Filters.powerCompMode = "=";
            }

            if (Filters.countCompMode == null)
            {
                Filters.countCompMode = "=";
            }

            if (Filters.capacityCompMode == null)
            {
                Filters.capacityCompMode = "=";
            }

            if (!powerIsValid)
            {
                var result = MessageBox.Show(
                $"Nelze uložit filtr; {PowerBox.Text} není platná hodnota ve W. Použijte prosím kladné číslo",
                "Chyba",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
                return;
            }
            else if (!countIsValid)
            {
                var result = MessageBox.Show(
                $"Nelze uložit filtr; {CountBox.Text} není platná hodnota množství. Použijte prosím kladné číslo",
                "Chyba",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
                return;
            }
            else if (!capacityIsValid)
            {
                var result = MessageBox.Show(
                $"Nelze uložit filtr; {CapacityBox.Text} není platná hodnota velikosti úložiště v GB. Použijte prosím kladné číslo",
                "Chyba",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
                return;
            }
            else
            {
                DialogResult = true;
            }
        }
        private void ChangeButtonBg(Button button, string path)
        {
            button.Background = new ImageBrush(new BitmapImage(new Uri(path)));
        }
    }
}
