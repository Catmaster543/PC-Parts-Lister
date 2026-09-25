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

namespace Pc_parts_lister.bin
{
    /// <summary>
    /// Interakční logika pro Edit_Parameters_window.xaml
    /// </summary>
    public partial class Edit_Parameters_window : Window
    {
        public ObservableCollection<PossibleParameter> PossibleParameters = new ObservableCollection<PossibleParameter>();
        public ICollectionView ParametersView { get; }
        public Edit_Parameters_window(ObservableCollection<PossibleParameter> possibleParameters)
        {
            InitializeComponent();

            PossibleParameters = possibleParameters;

            ParametersView = CollectionViewSource.GetDefaultView(PossibleParameters);
            DataContext = this;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            PossibleParameter possibleParameter = new PossibleParameter();
            possibleParameter.Name = "Nový parametr";
            possibleParameter.values = new List<string>();
            PossibleParameters.Add(possibleParameter);
            ParametersView.Refresh();
            Edit_Parameter_window Ewindow = new Edit_Parameter_window(possibleParameter);
            Ewindow.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is PossibleParameter parameter)
            {
                Edit_Parameter_window Ewindow = new Edit_Parameter_window(parameter);
                Ewindow.ShowDialog();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Pop-up
            Button button = (Button)sender;
            PossibleParameter parameter = (PossibleParameter)button.DataContext;
            PossibleParameters.Remove(parameter);
        }
    }
}
