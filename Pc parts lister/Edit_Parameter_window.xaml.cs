using System;
using System.Collections.Generic;
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
        public Edit_Parameter_window(PossibleParameter parameter)
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
        }

        private void List_String_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            kParameter.list = false;
        }

        private void List_String_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            kParameter.list = true;
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
            }
            else if (comboBox.SelectedItem.ToString() == "Number")
            {
                Int_Option_Panel.Visibility = Visibility.Visible;
                String_Option_Panel.Visibility = Visibility.Hidden;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (CustomAfterFix_panel.Visibility == Visibility.Visible && CustomAfterFix_Box.Text != "")
            {
                kParameter.intAfterFix = CustomAfterFix_Box.Text;
            } 
        }
    }
}
