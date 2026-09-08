using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
    public partial class Add_part : Window
    {
        public List<bool> boolChecks = new List<bool>();

        public Component Komponenta = new Component();

        

        public Add_part(ObservableCollection<PossibleParameter> possibleParameters)
        {
            InitializeComponent();

            //Intitial hiding of everything

            #region Hiding
            
            /*
            Manufacturertext.Visibility = Visibility.Collapsed;
            ManufacturerBox.Visibility = Visibility.Collapsed;
            SerText.Visibility = Visibility.Collapsed;
            SerBox.Visibility = Visibility.Collapsed;
            SubSerText.Visibility= Visibility.Collapsed;
            SubSerBox.Visibility = Visibility.Collapsed;
            ModelText.Visibility = Visibility.Collapsed;
            ModelBox.Visibility = Visibility.Collapsed;
            Powertext.Visibility = Visibility.Collapsed;
            PowerBox.Visibility = Visibility.Collapsed;
            CapacityText.Visibility = Visibility.Collapsed;
            CapacityBox.Visibility = Visibility.Collapsed;
            TypeText2.Visibility = Visibility.Collapsed;
            TypeBox2.Visibility = Visibility.Collapsed;
            */
            #endregion

            #region Setting default component parameters


            CountBox.TextChanged += Check_Int_Validation;

            #endregion

            //Default options for what is it

            bool isA = true;
            foreach (PossibleParameter parameter in possibleParameters)
            {
                if (isA && parameter.ID != "Type")
                {
                    Parameter_Column_A.Children.Add(CreatePanel(parameter));
                    isA = false;
                }
                else if (!isA && parameter.ID != "Type")
                {
                    Parameter_Column_B.Children.Add(CreatePanel(parameter));
                    isA = true;
                }
            }

            foreach (PossibleParameter parameter in possibleParameters)
            {
                if (parameter.ID == "Type")
                {
                    StackPanel panel = CreatePanel(parameter);
                    ComboBox comboBox = (ComboBox)panel.Children[1];
                    comboBox.ItemsSource = parameter.values;
                    Grid0.Children.Insert(0, panel);
                }
            }

            StatusBox.ItemsSource = new[]
            {
                "Funkční",
                "Ok",
                "Kritický",
                "Nefunkční",
                "Opravený"
            };
        }

        //Making component public

        

        public StackPanel CreatePanel(PossibleParameter parameter)
        {
            StackPanel panel = new StackPanel();
            TextBlock textBlock = new TextBlock();
            panel.Orientation = Orientation.Horizontal;
            panel.Tag = parameter;
            textBlock.Text = parameter.Name;
            textBlock.FontWeight = FontWeights.Bold;
            textBlock.FontSize = 20;
            panel.Children.Add(textBlock);
            if (parameter.type == Parameter.Type.String)
            {
                ComboBox comboBox = new ComboBox();
                comboBox.ItemsSource = parameter.values;
                comboBox.Margin = new Thickness(10,0,0,0);
                comboBox.BorderThickness = new Thickness(0);
                comboBox.FontSize = 20;

                panel.Children.Add(comboBox);

                return panel;
            }
            else if (parameter.type == Parameter.Type.Boolean)
            {
                Button button = new Button();

                panel.Children.Add(button);

                return panel;
            }
            else if (parameter.type == Parameter.Type.Number)
            {
                TextBox box = new TextBox();
                box.SelectionChanged += Check_Int_Validation;

                return panel;
            }
            else return null;
        }

        private void Check_Int_Validation(Object sender, RoutedEventArgs e)
        {
            bool result;
            TextBox box = (TextBox)sender;
            if (int.TryParse(box.Text, out int i))
            {
                result = true;
                box.Tag = i;
                box.Foreground = new SolidColorBrush(Colors.Black);
                box.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB3ABAB"));
            }
            else if (box.Text == null || box.Text == "")
            {
                result = true;
                box.Foreground = new SolidColorBrush(Colors.Black);
                box.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB3ABAB"));
            }
            else
            {
                result = false;
                box.Foreground = new SolidColorBrush(Colors.Red);
                box.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            boolChecks.Add(result);
        }

        private void Favorite_ButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Favorite_Button.BorderBrush = new SolidColorBrush(Colors.Green);
            ChangeFavButtonBg(button);
        }

        bool favBool = true;

        private void ChangeFavButtonBg(Button button)
        {
            if (favBool)
            {
                button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/favorite_button_on.png")));
            }
            else
            {
                button.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/favorite_button_off.png")));
            }
                
        }

        private Parameter convertPossibleToParameter(PossibleParameter possibleParameter)
        {
            Parameter parameter = new Parameter();
            parameter.Name = possibleParameter.Name;
            parameter.Value = possibleParameter.Value;
            parameter.type = possibleParameter.type;
            parameter.ID = possibleParameter.ID;

            return parameter;
        }

        /*
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
                CapacityText.Visibility= Visibility.Visible;
                CapacityBox.Visibility= Visibility.Visible;
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
                CapacityBox.Visibility = Visibility.Visible;
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
                PowerBox.Visibility = Visibility.Visible;
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

        private void StatusBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
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

        

        private void Power_TextChanged(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(PowerBox.Text, out int i))
            {
                powerIsLegit = true;
                powerIntT = i;
                Powertext.Foreground = new SolidColorBrush(Colors.Black);
                PowerBox.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB3ABAB"));
            }
            else if (PowerBox.Text == null || PowerBox.Text == "")
            {
                powerIsLegit = true;
                Powertext.Foreground = new SolidColorBrush(Colors.Black);
                PowerBox.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB3ABAB"));
            }
            else
            {
                powerIsLegit = false;
                Powertext.Foreground = new SolidColorBrush(Colors.Red);
                PowerBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }

        /*
        private void Count_TextChanged(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(QtBox.Text, out int i))
            {
                countIsLegit = true;
                countIntT = i;
                Qttext.Foreground = new SolidColorBrush(Colors.Black);
                QtBox.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB3ABAB"));
            }
            else if (QtBox.Text == null || QtBox.Text == "")
            {
                countIsLegit = true;
                Qttext.Foreground = new SolidColorBrush(Colors.Black);
                QtBox.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB3ABAB"));
            }
            else
            {
                countIsLegit = false;
                Qttext.Foreground = new SolidColorBrush(Colors.Red);
                QtBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }

        private void Capacity_TextChanged(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(CapacityBox.Text, out int i))
            {
                capacityIsLegit = true;
                capacityIntT = i;
                CapacityText.Foreground = new SolidColorBrush(Colors.Black);
                CapacityBox.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB3ABAB"));
            }
            else if (CapacityBox.Text == null || CapacityBox.Text == "")
            {
                capacityIsLegit = true;
                CapacityText.Foreground = new SolidColorBrush(Colors.Black);
                CapacityBox.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB3ABAB"));
            }
            else
            {
                capacityIsLegit = false;
                CapacityText.Foreground = new SolidColorBrush(Colors.Red);
                CapacityBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }
        */



        #region Saving the component

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            bool canSave = true;
            Komponenta.Name = NameBox.Text;
            if (StatusBox.SelectedItem != null)
            {
                Komponenta.Status = StatusBox.SelectedItem.ToString();
            }

            foreach (bool check in boolChecks)
            {
                if (!check)
                {
                    canSave = false;
                }
            }
            if (canSave)
            {
                foreach (StackPanel stackPanel in Parameter_Column_A.Children)
                {
                    Parameter currentParameter = (Parameter)stackPanel.Tag;
                    if (currentParameter.type == Parameter.Type.String)
                    {
                        ComboBox comboBox = (ComboBox)stackPanel.Children[1];
                        if (comboBox.SelectedItem != null)
                        {
                            //Clearing the placeholder parameter in favor of the new one
                            Komponenta.parameters.Remove(currentParameter);

                            currentParameter.Value = comboBox.SelectedItem.ToString();

                            Komponenta.parameters.Add(currentParameter);
                        }
                        else
                        {
                            Komponenta.parameters.Remove(currentParameter);
                        }
                    }
                    else if (currentParameter.type == Parameter.Type.Number)
                    {
                        Komponenta.parameters.Remove(currentParameter);
                        Komponenta.parameters.Add(currentParameter);
                    }
                    else
                    {
                        Komponenta.parameters.Remove(currentParameter);
                        Komponenta.parameters.Add(currentParameter);
                    }
                }

                foreach (StackPanel stackPanel in Parameter_Column_B.Children)
                {
                    Parameter currentParameter = (Parameter)stackPanel.Tag;

                    if (currentParameter.type == Parameter.Type.String)
                    {
                        ComboBox comboBox = (ComboBox)stackPanel.Children[1];
                        if (comboBox.SelectedItem != null)
                        {
                            //Clearing the placeholder parameter in favor of the new one
                            Komponenta.parameters.Remove(currentParameter);

                            currentParameter.Value = comboBox.SelectedItem.ToString();

                            Komponenta.parameters.Add(currentParameter);
                        }
                        else
                        {
                            Komponenta.parameters.Remove(currentParameter);
                        }
                    }
                    else
                    {
                        Komponenta.parameters.Remove(currentParameter);
                        Komponenta.parameters.Add(currentParameter);
                    }
                }
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                var result = MessageBox.Show(
                $"Nelze uložit komponentu; zkontrolujte zadané parametry",
                "Chyba",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
                return;
            }


            

            /*
            if (powerIsLegit && countIsLegit && capacityIsLegit)
            {
                if (TypeBox.Text == "CPU")
                {
                    Component = new Component
                    {
                        Type = TypeBox.SelectedItem?.ToString(),
                        SubType = TypeBox2.SelectedItem?.ToString(),
                        Manufacturer = ManufacturerBox.Text,
                        Series = SerBox?.SelectedItem?.ToString(),
                        SubSeries = SubSerBox?.SelectedItem?.ToString(),
                        Model = ModelBox.Text,
                        Quantity = countIntT,
                        Status = StatusBox.Text,

                        Name = ManufacturerBox.Text + " " + SerBox.Text + " " + SubSerBox.Text + " " + ModelBox.Text,
                    };
                }
                else if (TypeBox.Text == "Disk")
                {
                    Component = new Component
                    {
                        Type = TypeBox.SelectedItem?.ToString(),
                        SubType = TypeBox2.SelectedItem?.ToString(),
                        Manufacturer = ManufacturerBox.Text,
                        Model = ModelBox.Text,
                        Capacity = capacityIntT,
                        Quantity = countIntT,
                        Status = StatusBox.Text,

                        Name = ManufacturerBox.Text + " " + ModelBox.Text + " - " + CapacityBox.Text + "Gb",
                    };
                }
                else if (TypeBox.Text == "GPU")
                {
                    if (TypeBox2.Text == "Nvidia")
                    {
                        if (SerBox.Text == "RTX" || SerBox.Text == "GTX")
                        {
                            Component = new Component
                            {
                                Type = TypeBox.SelectedItem?.ToString(),
                                SubType = TypeBox2.SelectedItem?.ToString(),
                                Manufacturer = ManufacturerBox.Text,
                                Series = SerBox?.SelectedItem?.ToString(),
                                SubSeries = SubSerBox?.SelectedItem?.ToString(),
                                Model = ModelBox.Text,
                                Capacity = capacityIntT,
                                Quantity = countIntT,
                                Status = StatusBox.Text,

                                Name = ManufacturerBox.Text + " GeForce " + SerBox.Text + " " + ModelBox.Text,
                            };
                        }
                        else
                        {
                            Component = new Component
                            {
                                Type = TypeBox.SelectedItem?.ToString(),
                                SubType = TypeBox2.SelectedItem?.ToString(),
                                Manufacturer = ManufacturerBox.Text,
                                Series = SerBox?.SelectedItem?.ToString(),
                                SubSeries = SubSerBox?.SelectedItem?.ToString(),
                                Model = ModelBox.Text,
                                Capacity = capacityIntT,
                                Quantity = countIntT,
                                Status = StatusBox.Text,

                                Name = ManufacturerBox.Text + " " + TypeBox.Text + " " + SerBox.Text + " " + ModelBox.Text,
                            };
                        }
                    }
                    else
                    {
                        Component = new Component
                        {
                            Type = TypeBox.SelectedItem?.ToString(),
                            SubType = TypeBox2.SelectedItem?.ToString(),
                            Manufacturer = ManufacturerBox.Text,
                            Series = SerBox?.SelectedItem?.ToString(),
                            SubSeries = SubSerBox?.SelectedItem?.ToString(),
                            Model = ModelBox.Text,
                            Capacity = capacityIntT,
                            Quantity = countIntT,
                            Status = StatusBox.Text,

                            Name = ManufacturerBox.Text + " " + TypeBox2.Text + " " + ModelBox.Text,
                        };
                    }

                }
                else if (TypeBox.Text == "RAM")
                {
                    if (TypeBox2.Text == "DIMM")
                    {
                        Component = new Component
                        {
                            Type = TypeBox.SelectedItem?.ToString(),
                            SubType = TypeBox2.SelectedItem?.ToString(),
                            Series = SerBox?.SelectedItem?.ToString(),
                            Manufacturer = ManufacturerBox.Text,
                            Model = ModelBox.Text,
                            Capacity = capacityIntT,
                            Quantity = countIntT,
                            Status = StatusBox.Text,

                            Name = ManufacturerBox.Text + " " + ModelBox.Text + " (" + CapacityBox.Text + "Gb)",
                        };
                    }
                    else if (TypeBox2.Text == "SODIMM")
                    {
                        Component = new Component
                        {
                            Type = TypeBox.SelectedItem?.ToString(),
                            SubType = TypeBox2.SelectedItem?.ToString(),
                            Series = SerBox?.SelectedItem?.ToString(),
                            Manufacturer = ManufacturerBox.Text,
                            Model = ModelBox.Text,
                            Capacity = capacityIntT,
                            Quantity = countIntT,
                            Status = StatusBox.Text,

                            Name = ManufacturerBox.Text + " " + TypeBox2.Text + " " + ModelBox.Text + " (" + CapacityBox.Text + "Gb)",
                        };
                    }

                }
                else if (TypeBox.Text == "Mb")
                {
                    Component = new Component
                    {
                        Type = TypeBox.SelectedItem?.ToString(),
                        SubType = TypeBox2.SelectedItem?.ToString(),
                        Manufacturer = ManufacturerBox.Text,
                        Model = ModelBox.Text,
                        Quantity = countIntT,
                        Status = StatusBox.Text,

                        Name = ManufacturerBox.Text + " " + ModelBox.Text,
                    };
                }
                else if (TypeBox.Text == "PSU")
                {
                    Component = new Component
                    {
                        Type = TypeBox.SelectedItem?.ToString(),
                        SubType = TypeBox2.SelectedItem?.ToString(),
                        Manufacturer = ManufacturerBox.Text,
                        Model = ModelBox.Text,
                        Power = powerIntT,
                        Quantity = countIntT,
                        Status = StatusBox.Text,

                        Name = ManufacturerBox.Text + " " + ModelBox.Text + " " + PowerBox.Text + "w",
                    };
                }
                else if (TypeBox.Text == "Case")
                {
                    Component = new Component
                    {
                        Type = TypeBox.SelectedItem?.ToString(),
                        SubType = TypeBox2.SelectedItem?.ToString(),
                        Manufacturer = ManufacturerBox.Text,
                        Model = ModelBox.Text,
                        Power = powerIntT,
                        Quantity = countIntT,
                        Status = StatusBox.Text,

                        Name = ManufacturerBox.Text + " " + ModelBox.Text,
                    };
                }
                else
                {
                    Component = new Component
                    {
                        Type = TypeBox.SelectedItem?.ToString(),
                        SubType = TypeBox2.SelectedItem?.ToString(),
                        Manufacturer = ManufacturerBox.Text,
                        Series = SerBox?.SelectedItem?.ToString(),
                        SubSeries = SubSerBox?.SelectedItem?.ToString(),
                        Model = ModelBox.Text,
                        Capacity = capacityIntT,
                        Quantity = countIntT,
                        Status = StatusBox.Text,
                    };
                }
                this.DialogResult = true;
                this.Close();
            }
            else if (!powerIsLegit)
            {
                var result = MessageBox.Show(
                $"Nelze přidat součástku; {PowerBox.Text} není platná hodnota ve W. Použijte prosím kladné číslo",
                "Chyba",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
                return;
            }
            else if (!countIsLegit)
            {
                var result = MessageBox.Show(
                $"Nelze přidat součástku; {QtBox.Text} není platná hodnota pro množství. Použijte prosím kladné číslo",
                "Chyba",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
                return;
            }
            else
            {
                var result = MessageBox.Show(
                $"Nelze přidat součástku; něco je špatně, I just can't prove it yet",
                "Chyba",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
                return;
            }
        }

        private void CapacityBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        */
        }

        #endregion

        /*
        public Parameter FindParameter(Parameter parameter)
        {
            int i = 0;
            foreach (Parameter parameter in Komponenta.parameters)
            {
                if (Komponenta.parameters[i].ID == )
                
                i++;
            }
            return null;
        }
        */
    }
}
