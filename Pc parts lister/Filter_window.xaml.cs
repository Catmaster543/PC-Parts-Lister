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

            #region Setting default values to main boxes
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
            #endregion

            #region Loading filter values to keep the previous filters
            TypeBox.SelectedItem = filters.type;
            if (filters.type != null && filters.type != "all")
            {
                UnFilterType_Button.Visibility = Visibility.Visible;
            }
            StatusBox.SelectedItem = filters.status;
            if (filters.status != null)
            {
                UnFilterStatus_Button.Visibility = Visibility.Visible;
            }
            TypeBox2.SelectedItem = filters.type2;
            if (filters.type2 != null)
            {
                UnFilterType2_Button.Visibility = Visibility.Visible;
            }
            ManufacturerBox.Text = filters.manufacturer;
            if (filters.manufacturer != null)
            {
                UnFilterManufacturer_Button.Visibility = Visibility.Visible;
            }
            SerBox.SelectedItem = filters.series;
            if (filters.series != null)
            {
                UnFilterSeries_Button.Visibility = Visibility.Visible;
            }
            SubSerBox.SelectedItem = filters.subseries;
            if (filters.subseries != null)
            {
                UnFilterSubSer_Button.Visibility = Visibility.Visible;
            }
            ModelBox.Text = filters.model;
            if (filters.model != null)
            {
                UnFilterModel_Button.Visibility = Visibility.Visible;
            }
            if (filters.count != 0)
            {
                CountBox.Text = filters.count.ToString();
                UnFilterCount_Button.Visibility = Visibility.Visible;
            }
            if (filters.power != 0)
            {
                PowerBox.Text = filters.power.ToString();
                UnFilterPower_Button.Visibility = Visibility.Visible;
            }
            if (filters.capacity != 0)
            {
                CapacityBox.Text = filters.capacity.ToString();
                UnFilterCapacity_Button.Visibility = Visibility.Visible;
            }

            SetCompButtonOutline(CountGrid, filters.countCompMode);
            SetCompButtonOutline(PowerGrid, filters.powerCompMode);
            SetCompButtonOutline(CapacityGrid, filters.capacityCompMode);

            if (filters.filterFavorite)
            {
                UnFilterFavorite_Button.Visibility = Visibility.Visible;
            }
            #endregion
        }

        public Component Component { get; private set; }

        private void TypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var type = TypeBox.SelectedItem?.ToString();
            Manufacturertext.Visibility = Visibility.Visible;
            ManufacturerBox.Visibility = Visibility.Visible;

            UnFilterType_Button.Visibility = Visibility.Visible;

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

            UnFilterType2_Button.Visibility = Visibility.Visible;

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

            UnFilterManufacturer_Button.Visibility = Visibility.Visible;

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

        private void SerBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var Series = SerBox.SelectedItem?.ToString();

            UnFilterSeries_Button.Visibility = Visibility.Visible;

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

        private void ModelBox_TextChanged(object sender, RoutedEventArgs e)
        {
            UnFilterModel_Button.Visibility = Visibility.Visible;
        }

        private void StatusBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UnFilterStatus_Button.Visibility = Visibility.Visible;
        }

        #region Cpu parameters
        private void SubSerBox_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            ModelText.Visibility = Visibility.Visible;
            ModelBox.Visibility = Visibility.Visible;

            UnFilterSubSer_Button.Visibility = Visibility.Visible;
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
                    ClearAnError(ref powerErrorInt, true);
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
            UnFilterPower_Button.Visibility = Visibility.Visible;
        }
        private void Power_Smaller_Click(object sender, RoutedEventArgs e)
        {
            Filters.powerCompMode = "<";
            if (powerCompErrorInt != 0)
            {
                ClearAnError(ref powerCompErrorInt);
            }
            SetCompButtonOutline(PowerGrid, Filters.powerCompMode);
        }
        private void Power_Bigger_Click(object sender, RoutedEventArgs e)
        {
            Filters.powerCompMode = ">";
            if (powerCompErrorInt != 0)
            {
                ClearAnError(ref powerCompErrorInt);
            }
            SetCompButtonOutline(PowerGrid, Filters.powerCompMode);
        }
        private void Power_Same_Click(object sender, RoutedEventArgs e)
        {
            Filters.powerCompMode = "=";
            if (powerCompErrorInt != 0)
            {
                ClearAnError(ref powerCompErrorInt);
            }
            SetCompButtonOutline(PowerGrid, Filters.powerCompMode);
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
                    ClearAnError(ref countErrorInt, true);
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
                    AlterAnError(countErrorInt, $"{ CountBox.Text} není platná hodnota počtu, použijte kladné číslo.");
                }
                CountText.Foreground = new SolidColorBrush(Colors.Red);
                CountBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            UnFilterCount_Button.Visibility = Visibility.Visible;
        }
        private void Count_Smaller_Click(object sender, RoutedEventArgs e)
        {
            Filters.countCompMode = "<";
            if (countCompErrorInt != 0)
            {
                ClearAnError(ref countCompErrorInt);
            }
            SetCompButtonOutline(CountGrid, Filters.countCompMode);
        }
        private void Count_Bigger_Click(object sender, RoutedEventArgs e)
        {
            Filters.countCompMode = ">";
            if (countCompErrorInt != 0)
            {
                ClearAnError(ref countCompErrorInt);
            }
            SetCompButtonOutline(CountGrid, Filters.countCompMode);
        }
        private void Count_Same_Click(object sender, RoutedEventArgs e)
        {
            Filters.countCompMode = "=";
            if (countCompErrorInt != 0)
            {
                ClearAnError(ref countCompErrorInt);
            }
            SetCompButtonOutline(CountGrid, Filters.countCompMode);
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
            UnFilterCapacity_Button.Visibility = Visibility.Visible;
        }
        private void Capacity_Smaller_Click(object sender, RoutedEventArgs e)
        {
            Filters.capacityCompMode = "<";
            if (capacityCompErrorInt != 0)
            {
                ClearAnError(ref capacityCompErrorInt);
            }
            SetCompButtonOutline(CapacityGrid, Filters.capacityCompMode);
        }
        private void Capacity_Bigger_Click(object sender, RoutedEventArgs e)
        {
            Filters.capacityCompMode = ">";
            if (capacityCompErrorInt != 0)
            {
                ClearAnError(ref capacityCompErrorInt);
            }
            SetCompButtonOutline(CapacityGrid, Filters.capacityCompMode);
        }
        private void Capacity_Same_Click(object sender, RoutedEventArgs e)
        {
            Filters.capacityCompMode = "=";
            if (capacityCompErrorInt != 0)
            {
                ClearAnError(ref capacityCompErrorInt);
            }
            SetCompButtonOutline(CapacityGrid, Filters.capacityCompMode);
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

        #region Clearing specified filters reacts
        private void UnFilterFavorite_Click(object sender, RoutedEventArgs e)
        {
            Filters.filterFavorite = false;
            Filters.favorite = false;
            Favorite_Button.BorderBrush = null;
            UnFilterFavorite_Button.Visibility = Visibility.Collapsed;
            ChangeButtonBg(Favorite_Button, Filters.favoriteIconPath);
        }

        private void UnFilterType_Click(object sender, RoutedEventArgs e)
        {
            Filters.type = null;
            TypeBox.SelectedItem = null;
            UnFilterType_Button.Visibility = Visibility.Collapsed;
        }

        private void UnFilterManufacturer_Click(object sender, RoutedEventArgs e)
        {
            Filters.manufacturer = null;
            ManufacturerBox.Text = null;
            UnFilterManufacturer_Button.Visibility = Visibility.Collapsed;
        }

        private void UnFilterStatus_Click(object sender, RoutedEventArgs e)
        {
            Filters.status = null;
            StatusBox.SelectedItem = null;
            UnFilterStatus_Button.Visibility = Visibility.Collapsed;
        }

        private void UnFilterCount_Click(object sender, RoutedEventArgs e)
        {
            Filters.count = 0;
            CountBox.Text = null;
            Filters.countCompMode = null;
            SetCompButtonOutline(CountGrid, Filters.countCompMode);
            UnFilterCount_Button.Visibility = Visibility.Collapsed;
        }

        private void UnFilterType2_Click(object sender, RoutedEventArgs e)
        {
            Filters.type2 = null;
            TypeBox2.Text = null;
            UnFilterType2_Button.Visibility = Visibility.Collapsed;
        }

        private void UnFilterSeries_Click(object sender, RoutedEventArgs e)
        {
            Filters.series = null;
            SerBox.SelectedItem = null;
            UnFilterSeries_Button.Visibility = Visibility.Collapsed;
        }

        private void UnFilterSubSer_Click(object sender, RoutedEventArgs e)
        {
            Filters.subseries = null;
            SubSerBox.SelectedItem = null;
            UnFilterSubSer_Button.Visibility = Visibility.Collapsed;
        }

        private void UnFilterModel_Click(object sender, RoutedEventArgs e)
        {
            Filters.model = null;
            ModelBox.Text = null;
            UnFilterModel_Button.Visibility = Visibility.Collapsed;
        }

        private void UnFilterPower_Click(object sender, RoutedEventArgs e)
        {
            Filters.power = 0;
            PowerBox.Text = null;
            Filters.powerCompMode = null;
            SetCompButtonOutline(PowerGrid, Filters.powerCompMode);
            UnFilterPower_Button.Visibility = Visibility.Collapsed;
        }

        private void UnFilterCapacity_Click(object sender, RoutedEventArgs e)
        {
            Filters.capacity = 0;
            CapacityBox.Text = null;
            Filters.capacityCompMode = null;
            SetCompButtonOutline(CapacityGrid, Filters.capacityCompMode);
            UnFilterCapacity_Button.Visibility = Visibility.Collapsed;
        }
        #endregion

        private void SetCompButtonOutline(Grid grid, string sourceCompareMode)
        {
            for (int i = 0; i < grid.Children.Count; i++)
            {
                if (grid.Children[i] is Button)
                {
                    Button button = grid.Children[i] as Button;

                    if (button.Name.Contains("Smaller") && sourceCompareMode == "<")
                    {
                        button.BorderBrush = new SolidColorBrush(Colors.GreenYellow);
                    }
                    else if (button.Name.Contains("Bigger") && sourceCompareMode == ">")
                    {
                        button.BorderBrush = new SolidColorBrush(Colors.GreenYellow);
                    }
                    else if (button.Name.Contains("Equal") && sourceCompareMode == "=")
                    {
                        button.BorderBrush = new SolidColorBrush(Colors.GreenYellow);
                    }
                    else
                    {
                        button.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF707070"));
                    }
                }
            }
        }
        
        private void Save_Filter_Click(object sender, RoutedEventArgs e)
        {
            if (TypeBox.Text == "CPU")
            {
                Filters.type = TypeBox.SelectedItem.ToString();
                if (ManufacturerBox.SelectedItem != null)
                {
                    Filters.manufacturer = ManufacturerBox.SelectedItem.ToString();
                }
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

            if (SubSerBox.Text != null && SubSerBox.Text != "Vyber, nebo napiš vlastní")
            {
                Filters.subseries = SubSerBox.Text;
            }

            if (TypeBox2.SelectedItem != null)
            {
                Filters.type2 = TypeBox2.SelectedItem.ToString();
            }

            if (ModelBox.Text != null || ModelBox.Text == "")
            {
                Filters.model = ModelBox.Text;
            }

            if (Filters.powerCompMode == null && PowerBox.Text != "")
            {
                Filters.powerCompMode = "=";
            }

            if (Filters.countCompMode == null && CountBox.Text != "")
            {
                Filters.countCompMode = "=";
            }

            if (Filters.capacityCompMode == null &&  CapacityBox.Text != "")
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
