using Pc_parts_lister.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace Pc_parts_lister
{
    /// <summary>
    /// Interakční logika pro Edit_Parameter_window.xaml
    /// </summary>
    public partial class Edit_Parameter_window : Window
    {
        public bool creatingParameter;
        bool switchedByCode = true;
        public PossibleParameter kParameter;
        public ICollectionView ValuesView { get; set; }
        private Parameter.Type selectedType;
        public Edit_Parameter_window(PossibleParameter parameter, bool creatingParameter)
        {
            InitializeComponent();

            kParameter = parameter;

            Name_Box.Text = parameter.Name;
            ID_Box.Text = parameter.ID;
            List<string> content = new List<string>();
            content.Add(PossibleParameter.Type.String.ToString());
            content.Add(PossibleParameter.Type.Number.ToString());
            Type_Box.ItemsSource = content;
            Type_Box.SelectedItem = parameter.type.ToString();

            if (parameter.list)
            {
                ListString_CheckBox.IsChecked = true;

                if (parameter.customWriting)
                {
                    CustomString_CheckBox.IsChecked = true;
                }
            }

            if (parameter.intSufix != null && parameter.intSufix != "")
            {
                CustomSuffixCheckBox.IsChecked = true;
                CustomAfterFix_Box.Text = parameter.intSufix;
            } 

            if (parameter.values == null)
            {
                EditValue_Panel.Visibility = Visibility.Hidden;
            }

            if (parameter.list)
            {
                if (parameter.values == null)
                {
                    parameter.values = new List<string>();
                }
                ValuesView = CollectionViewSource.GetDefaultView(parameter.values);
            }

            DataContext = this;
            this.creatingParameter = creatingParameter;
        }

        private void List_String_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            kParameter.list = false;
            Values_Panel.Visibility = Visibility.Hidden;
            kParameter.customWriting = false;
            CustomStringAllowed_Panel.Visibility = Visibility.Hidden;
        }

        private void List_String_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            kParameter.list = true;
            if (kParameter.values == null)
            {
                kParameter.values = new List<string>();
                ValuesView = CollectionViewSource.GetDefaultView(kParameter.values);
            }
            Values_Panel.Visibility = Visibility.Visible;
            CustomStringAllowed_Panel.Visibility = Visibility.Visible;
        }
        private void Custom_String_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            kParameter.customWriting = false;
        }

        private void Custom_String_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            kParameter.customWriting = true;
        }
        private void CustomAfterFix_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CustomAfterFix_panel.Visibility = Visibility.Hidden;
        }

        private void CustomAfterFix_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CustomAfterFix_panel.Visibility = Visibility.Visible;
        }

        private void TypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (!creatingParameter && !switchedByCode)
            {
                MessageBoxResult result = MessageBox.Show(Strings.TypeSwapWarning, Strings.Warning, MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    if (comboBox.SelectedItem.ToString() == "String")
                    {
                        String_Option_Panel.Visibility = Visibility.Visible;
                        Int_Option_Panel.Visibility = Visibility.Hidden;

                        selectedType = Parameter.Type.String;
                    }
                    else if (comboBox.SelectedItem.ToString() == "Number")
                    {
                        Int_Option_Panel.Visibility = Visibility.Visible;
                        String_Option_Panel.Visibility = Visibility.Hidden;

                        selectedType = Parameter.Type.Number;
                    }
                }
                else
                {
                    switchedByCode = true;
                    if (comboBox.SelectedItem.ToString() == "String")
                    {
                        comboBox.SelectedItem = "Number";
                        Int_Option_Panel.Visibility = Visibility.Visible;
                        String_Option_Panel.Visibility = Visibility.Hidden;
                        selectedType = Parameter.Type.Number;
                    }   
                    else if (comboBox.SelectedItem.ToString() == "Number")
                    {
                        comboBox.SelectedItem = "String";
                        String_Option_Panel.Visibility = Visibility.Visible;
                        Int_Option_Panel.Visibility = Visibility.Hidden;
                        selectedType = Parameter.Type.String;
                    }
                }
            }
            else if (creatingParameter)
            {
                if (comboBox.SelectedItem.ToString() == "String")
                {
                    String_Option_Panel.Visibility = Visibility.Visible;
                    Int_Option_Panel.Visibility = Visibility.Hidden;

                    selectedType = Parameter.Type.String;
                }
                else if (comboBox.SelectedItem.ToString() == "Number")
                {
                    Int_Option_Panel.Visibility = Visibility.Visible;
                    String_Option_Panel.Visibility = Visibility.Hidden;

                    selectedType = Parameter.Type.Number;
                }
            }
            else if (switchedByCode)
            {
                switchedByCode = false;
                if (comboBox.SelectedItem.ToString() == "String")
                {
                    Int_Option_Panel.Visibility = Visibility.Hidden;
                    String_Option_Panel.Visibility = Visibility.Visible;
                    selectedType = Parameter.Type.String;
                }
                else if (comboBox.SelectedItem.ToString() == "Number")
                {
                    String_Option_Panel.Visibility = Visibility.Hidden;
                    Int_Option_Panel.Visibility = Visibility.Visible;
                    selectedType = Parameter.Type.Number;
                }
                return;
            }
        }

        private void ValuesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox box = sender as ListBox;
            if (box.SelectedItem != null)
            {
                EditValue_Panel.Visibility = Visibility.Visible;
                Value_Box.Text = box.SelectedItem.ToString();
            }
            else
            {
                EditValue_Panel.Visibility = Visibility.Hidden;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            string value = (string)button.DataContext;

            kParameter.values.Remove(value);
            ValuesView.Refresh();
        }

        private void AddValue_Click(object sender, RoutedEventArgs e)
        {
            string newValue = Strings.NewValue;
            kParameter.values.Add(newValue);
            ValuesView.Refresh();
            ValuesListBox.SelectedItem = newValue;
        }

        private void ConfirmEditButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValuesListBox.SelectedItem != null)
            {
                kParameter.values.Remove(ValuesListBox.SelectedItem.ToString());
                kParameter.values.Add(Value_Box.Text);
                ValuesView.Refresh();
                ValuesListBox.SelectedItem = Value_Box.Text;
                Value_Box.Text = string.Empty;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (CustomAfterFix_panel.Visibility == Visibility.Visible && CustomAfterFix_Box.Text != "")
            {
                kParameter.intSufix = CustomAfterFix_Box.Text;
            }
            kParameter.Name = Name_Box.Text;
            kParameter.ID = ID_Box.Text;
            kParameter.type = selectedType;
            // Custom parameters for string & booleans are handled in their respective checkboxes separately.
            this.DialogResult = true;
            this.Close();
        }
    }
}
