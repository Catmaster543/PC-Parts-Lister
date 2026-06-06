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
            //ComponentsView.Filter = FilterComponentsByType;
            ComponentsView.Filter = FilterComponents;

            DataContext = this;


        }

        string selectedType;

        string inputText;
        bool searchBool;
        bool typeBool;
        private bool FilterComponents(object obj)   //Main method for filtering components
        {
            searchBool = false;
            typeBool = false;
            if (obj == null)
                return false;

            Component component = obj as Component;
            if (component == null)
                return false;

            if (selectedType == "all" && (inputText == "" || inputText == null))
            {
                return true;
            }

            if (inputText != null)
            {
                if (component.Name.ToLower().Contains(inputText.ToLower()))
                {
                    searchBool = true;
                }
                if (selectedType == "all")
                {
                    typeBool = true;
                }
            }
            if (inputText == null || inputText == "")
            {
                searchBool = true;
            }

            if (selectedType == component.Type)
            {
                typeBool = true;
            }

            return searchBool && typeBool;
        }

        private void Filtering_Changed(object sender, RoutedEventArgs e)
        {
            inputText = Search_Box.Text;
            ComponentsView.Refresh();
        }

        private void Hledat_click(object sender, RoutedEventArgs e)
        {
            Lookup.Visibility = Visibility.Collapsed;
            selectedType = "all";
            ComponentsView.Refresh();
            Search.Visibility = Visibility.Visible;
        }

        private void Mb_click(object sender, RoutedEventArgs e)
        {
            Lookup.Visibility = Visibility.Collapsed;
            selectedType = "Mb";
            ComponentsView.Refresh();
            Search.Visibility = Visibility.Visible;
        }

        private void Cpu_click(object sender, RoutedEventArgs e)
        {
            Lookup.Visibility = Visibility.Collapsed;
            selectedType = "CPU";
            ComponentsView.Refresh();
            Search.Visibility = Visibility.Visible;
        }

        private void Ram_click(object sender, RoutedEventArgs e)
        {
            Lookup.Visibility = Visibility.Collapsed;
            selectedType = "RAM";
            ComponentsView.Refresh();
            Search.Visibility = Visibility.Visible;
        }

        private void Gpu_click(object sender, RoutedEventArgs e)
        {
            Lookup.Visibility = Visibility.Collapsed;
            selectedType = "GPU";
            ComponentsView.Refresh();
            Search.Visibility = Visibility.Visible;
        }

        private void Psu_click(object sender, RoutedEventArgs e)
        {
            Lookup.Visibility = Visibility.Collapsed;
            selectedType = "PSU";
            ComponentsView.Refresh();
            Search.Visibility = Visibility.Visible;
        }

        private void Disk_click(object sender, RoutedEventArgs e)
        {
            Lookup.Visibility = Visibility.Collapsed;
            selectedType = "Disk";
            ComponentsView.Refresh();
            Search.Visibility = Visibility.Visible;
        }

        private void Case_click(object sender, RoutedEventArgs e)
        {
            Lookup.Visibility = Visibility.Collapsed;
            selectedType = "Case";
            ComponentsView.Refresh();
            Search.Visibility = Visibility.Visible;
        }

        private void Other_click(object sender, RoutedEventArgs e)
        {
            Lookup.Visibility = Visibility.Collapsed;
            selectedType = "Jiné";
            ComponentsView.Refresh();
            Search.Visibility = Visibility.Visible;
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
