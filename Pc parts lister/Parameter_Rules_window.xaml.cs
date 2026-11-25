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
                Value_Panel.Visibility = Visibility.Visible;
                PossibleParameter parameter = box.SelectedItem as PossibleParameter;
                Value_ComboBox.ItemsSource = parameter.values;
                Value_ComboBox.Tag = parameter;
            }
            else
            {
                Value_Panel.Visibility = Visibility.Collapsed;
            }
        }

        public void ValueComboBox_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            ComboBox box = (ComboBox)sender;
            foreach (ParameterRule rule in parameterRules)
            {
                
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
