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
    public partial class Lookup_window : Window
    {
        //public Component Component { get; }
        public ObservableCollection<Component> Components { get; }
        public ICollectionView ComponentsView { get; }

        public Lookup_window(ObservableCollection<Component> components)
        {
            InitializeComponent();
            
            
            Components = components;
            ComponentsView = CollectionViewSource.GetDefaultView(Components);
            ComponentsView.Filter = FilterComponents;

            DataContext = this;
        }

        private bool FilterComponents(object obj)
        {
            if (obj == null)
                return false;

            Component component = obj as Component;
            if (component == null)
                return false;

            if (TypeFilterBox.SelectedItem == null)
                return true;

            string selectedType = TypeFilterBox.SelectedItem.ToString();
            return component.Type == selectedType;
        }

        private void TypeFilterBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComponentsView.Refresh();
        }

        private void Hledat_click(object sender, RoutedEventArgs e)
        {
            Lookup.Visibility = Visibility.Collapsed;
            Search.Visibility = Visibility.Visible;
        }

        private void Mb_click(object sender, RoutedEventArgs e)
        {

        }

        private void Cpu_click(object sender, RoutedEventArgs e)
        {

        }

        private void Ram_click(object sender, RoutedEventArgs e)
        {

        }

        private void Gpu_click(object sender, RoutedEventArgs e)
        {

        }

        private void Psu_click(object sender, RoutedEventArgs e)
        {

        }

        private void Case_click(object sender, RoutedEventArgs e)
        {

        }

        private void Other_click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Component komponenta)
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

        //Search section
        private void DetailButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Component komponenta)
            {
                DetailWindow Dwindow = new DetailWindow(komponenta);
                Dwindow.ShowDialog();
            }
        }


        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Component komponenta)
            {
                Edit_window Ewindow = new Edit_window(komponenta);
                Ewindow.ShowDialog();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Search.Visibility = Visibility.Collapsed;
            Lookup.Visibility = Visibility.Visible;
        }
    }
}
