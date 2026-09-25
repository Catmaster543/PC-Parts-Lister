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
        public PossibleParameter kParameter;
        public ICollectionView ValuesView { get; }
        private Parameter.Type selectedType;
        public Edit_Parameter_window(PossibleParameter parameter)
        {
            InitializeComponent();

            kParameter = parameter;

            ValuesView = CollectionViewSource.GetDefaultView(parameter.values);

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

            DataContext = this;
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

        private void ValuesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox box = sender as ListBox;
            if (box.SelectedItem != null)
            {
                Value_Box.Text = box.SelectedItem.ToString();
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
            string newValue = "Nová hodnota";
            kParameter.values.Add(newValue);
            ValuesView.Refresh();
            ValuesListBox.SelectedItem = newValue;
        }

        private void ConfirmEditButton_Click(object sender, RoutedEventArgs e)
        {
            kParameter.values.Remove(ValuesListBox.SelectedItem.ToString());
            kParameter.values.Add(Value_Box.Text);
            ValuesView.Refresh();
            ValuesListBox.SelectedItem = Value_Box.Text;
            Value_Box.Text = string.Empty;
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
