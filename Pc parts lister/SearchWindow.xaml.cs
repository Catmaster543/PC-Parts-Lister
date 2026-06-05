using Pc_parts_lister;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interakční logika pro SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        public ObservableCollection<Component> Components { get; }
        public SearchWindow(ObservableCollection<Component> components)
        {
            InitializeComponent();
            Components = components;
            DataContext = this;
        }

        

         private void DeleteButton_Click(object sender, RoutedEventArgs e)
         {
            if (sender is Button button &&
                button.DataContext is Component komponenta)
            {
                var result = MessageBox.Show(
                $"Opravdu chceš smazat {komponenta.Name}?",
                "Potvrzení smazání",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    //MainWindow.Remove(komponenta);
                    Components.Remove(komponenta);
                }
            }
         }


        private void DetailButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button &&
        button.DataContext is Component komponenta)
            {
                DetailWindow Dwindow = new DetailWindow(komponenta);
                Dwindow.ShowDialog();
            }
        }
        

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button &&
        button.DataContext is Component komponenta)
            {
                Edit_window Ewindow = new Edit_window(komponenta);
                Ewindow.ShowDialog();
            }
        }
    }
}
