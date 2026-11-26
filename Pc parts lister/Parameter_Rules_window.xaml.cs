using Pc_parts_lister.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interakční logika pro ParameterRules.xaml
    /// </summary>
    public partial class Parameter_Rules_window : Window
    {
        public ObservableCollection<ParameterRule> parameterRules = new ObservableCollection<ParameterRule>();
        public ObservableCollection<PossibleParameter> possibleParameters = new ObservableCollection<PossibleParameter>();

        public ICollectionView ParameterRulesView { get; }
        public Parameter_Rules_window(ObservableCollection<PossibleParameter> PossibleParameters, PossibleParameter parameter)
        {
            InitializeComponent();

            possibleParameters = PossibleParameters;

            ParameterRulesView = CollectionViewSource.GetDefaultView(parameterRules);

            Parameter_ComboBox.ItemsSource = possibleParameters;
            Parameter_ComboBox.DisplayMemberPath = "Name";

            DataContext = this;
        }

        public void AddParameterRule_Click(object sender, RoutedEventArgs e)
        {
            ParameterRule newRule = new ParameterRule();

            parameterRules.Add(newRule);
            ParameterRulesView.Refresh();
        }

        public void DeleteButton_Click(Object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            parameterRules.Remove(button.DataContext as ParameterRule);
        }

        public void ParameterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox box = (ComboBox)sender;
            if (box.SelectedItem != null)
            {
                ParameterRule newRule = ValuesListBox.SelectedItem as ParameterRule;
                newRule.dependentParameter = box.SelectedItem as PossibleParameter;
                Value_Panel.Visibility = Visibility.Visible;
                PossibleParameter parameter = box.SelectedItem as PossibleParameter;
                if (parameter.type == Parameter.Type.String)
                {
                    ComboBoxPType_Panel.Visibility = Visibility.Visible;
                    TextPType_Panel.Visibility = Visibility.Collapsed;
                    if (parameter.customWriting)
                    {
                        Value_ComboBox.IsEditable = true;
                    }
                    else
                    {
                        Value_ComboBox.IsEditable = false;
                    }
                    Value_ComboBox.ItemsSource = parameter.values;
                    Value_ComboBox.Tag = parameter;
                }
                else if (parameter.type == Parameter.Type.Number)
                {
                    TextPType_Panel.Visibility = Visibility.Visible;
                    ComboBoxPType_Panel.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                Value_Panel.Visibility = Visibility.Collapsed;
            }
        }

        public void ValueComboBox_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            ComboBox box = (ComboBox)sender;
            ParameterRule rule = ValuesListBox.SelectedItem as ParameterRule;
            if (rule.dependentParameter.type == Parameter.Type.String)
            {
                rule.value = box.SelectedItem.ToString();
            }
        }

        public void ValueTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            ParameterRule rule = ValuesListBox.SelectedItem as ParameterRule;
            if (int.TryParse(box.Text, out int result))
            {
                rule.value = box.Text;
                box.BorderBrush = new SolidColorBrush(Colors.Black);
                box.Foreground = new SolidColorBrush(Colors.Black);
            }
            else
            {
                box.BorderBrush = new SolidColorBrush(Colors.Red);
                box.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        public void RulesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox box = (ListBox)sender;
            if (box.SelectedItem != null)
            {
                Edit_Grid.Visibility = Visibility.Visible;
            }
            else
            {
                Edit_Grid.Visibility = Visibility.Collapsed;
            }
        }
    }
}
