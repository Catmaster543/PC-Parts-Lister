using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting;
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

        private SearchFilters Filters = new SearchFilters();

        private ObservableCollection<PossibleParameter> PossibleParameters = new ObservableCollection<PossibleParameter>();

        public ObservableCollection<FilterParameter> filterParameters = new ObservableCollection<FilterParameter>();

        public Lookup_window(ObservableCollection<Component> components, ObservableCollection<PossibleParameter> possibleParameters)
        {
            InitializeComponent();

            PossibleParameters = possibleParameters;

            Components = components;
            ComponentsView = CollectionViewSource.GetDefaultView(Components);
            
            ComponentsView.Filter = FilterComponents;

            DataContext = this;
        }

        public class SearchFilters
        {
            public string type { get; set; }
            public string manufacturer {  get; set; }
            public string status { get; set; }
            public string series { get; set; }
            public string subseries { get; set; }
            public string type2 { get; set; }
            public string model { get; set; }
            public string powerCompMode { get; set; }
            public int power { get; set; }
            public string countCompMode { get; set; }
            public int count { get; set; }
            public string capacityCompMode { get; set; }
            public int capacity { get; set; }
            public bool favorite { get; set; }
            public string favoriteIconPath
            {
                get
                {
                    if (favorite)
                    {
                        return "pack://application:,,,/favorite_button_on.png";
                    }
                    else
                    {
                        return "pack://application:,,,/favorite_button.png";
                    }
                }
            }
            public bool filterFavorite {  get; set; }
        }

        string inputText;
        private bool FilterComponents(object obj)   //Main method for filtering components
        {
            bool result = true;
            Component komponenta = obj as Component;
            if (komponenta == null)
            {
                return false;
            }
            foreach (FilterParameter filterParameter in filterParameters)
            {
                bool found = false;
                foreach (Parameter parameter in komponenta.parameters)
                {
                    if (parameter.ID == filterParameter.ID)
                    {
                        found = true;
                        if (parameter.type == Parameter.Type.Number)
                        {
                            if (int.TryParse(parameter.Value, out int pi) && int.TryParse(filterParameter.Value, out int fi))
                            {
                                if (filterParameter.numberFilterType == FilterParameter.NumberFilterType.Equals)
                                {
                                    if (pi == fi)
                                    {
                                        //return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                else if (filterParameter.numberFilterType == FilterParameter.NumberFilterType.Smaller)
                                {
                                    if (pi <= fi)
                                    {
                                        //return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                else if (filterParameter.numberFilterType == FilterParameter.NumberFilterType.Bigger)
                                {
                                    if (pi >= fi)
                                    {
                                        //return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                        else if (parameter.type == Parameter.Type.String)
                        {
                            if (parameter.Value == filterParameter.Value)
                            {
                                //return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        break;
                    }
                    
                    /*else
                    {
                        return false;
                    }
                    */
                }
                if (!found)
                {
                    return false;
                }
            }

            return true;
        }
/*
            searchBool = false;
            typeBool = false;
            manuBool = false;
            statusBool = false;
            serBool = false;
            type2Bool = false;
            modelBool = false;
            powerBool = false;
            countBool = false;
            capacityBool = false;
            favoriteBool = false;
            subSerBool = false;

            if (obj == null)
                return false;

            Component component = obj as Component;
            if (component == null)
                return false;

            if (Filters.type == "all" && (inputText == "" || inputText == null))
            {
                return true;
            }

            if (inputText != null)
            {
                if (component.Name.ToLower().Contains(inputText.ToLower()))
                {
                    searchBool = true;
                }
                if (Filters.type == "all")
                {
                    typeBool = true;
                }
            }
            if (inputText == null || inputText == "")
            {
                searchBool = true;
            }

            return false;
            */
            /*
            if (Filters.type == component.Type)
            {
                typeBool = true;
            }
            else if (Filters.type == null)
            {
                typeBool = true;
            }

            if (Filters.manufacturer != null)
            {
                if (component.Manufacturer.ToLower().Contains(Filters.manufacturer.ToLower()))
                {
                    manuBool = true;
                }
            }
            else if (Filters.manufacturer == null)
            {
                manuBool = true;
            }

            if (Filters.status == component.Status || Filters.status == null)
            {
                statusBool = true;
            }

            if (Filters.series == component.Series || Filters.series == null)
            {
                serBool = true;
            }

            if (Filters.subseries == component.SubSeries || Filters.subseries == null)
            {
                subSerBool = true;
            }

            if (Filters.type2 == component.SubType || Filters.type2 == null)
            {
                type2Bool = true;
            }

            if (Filters.model != null)
            {
                if (component.Model.ToLower().Contains(Filters.model.ToLower()))
                {
                    modelBool = true;
                }
            }
            else if (Filters.model == null)
            {
                modelBool = true;
            }

            if (Filters.power != 0 && component.Power != 0)
            {
                if (Filters.powerCompMode == "<")
                {
                    if (component.Power <= Filters.power)
                    {
                        powerBool = true;
                    }
                    else 
                    {
                        powerBool = false; 
                    }
                }
                else if (Filters.powerCompMode == ">")
                {
                    if (component.Power >= Filters.power)
                    {
                        powerBool = true;
                    }
                    else
                    {
                        powerBool = false;
                    }
                }
                else if (Filters.powerCompMode == "=")
                {
                    if (component.Power == Filters.power)
                    {
                        powerBool = true;
                    }
                    else
                    {
                        powerBool = false;
                    }
                }
                else
                {
                    powerBool = false;
                }
            }
            else 
            {
                powerBool = true;
            }

            if (Filters.count != 0 && component.Quantity != 0)
            {
                if (Filters.countCompMode == "<")
                {
                    if (component.Quantity <= Filters.count)
                    {
                        countBool = true;
                    }
                    else
                    {
                        countBool = false;
                    }
                }
                else if (Filters.countCompMode == ">")
                {
                    if (component.Quantity >= Filters.count)
                    {
                        countBool = true;
                    }
                    else
                    {
                        countBool = false;
                    }
                }
                else if (Filters.countCompMode == "=")
                {
                    if (component.Quantity == Filters.count)
                    {
                        countBool = true;
                    }
                    else
                    {
                        countBool = false;
                    }
                }
                else
                {
                    countBool = false;
                }
            }
            else
            {
                countBool = true;
            }

            if (Filters.capacity != 0 && component.Capacity != 0)
            {
                if (Filters.capacityCompMode == "<")
                {
                    if (component.Capacity <= Filters.capacity)
                    {
                        capacityBool = true;
                    }
                    else
                    {
                        capacityBool = false;
                    }
                }
                else if (Filters.capacityCompMode == ">")
                {
                    if (component.Capacity >= Filters.capacity)
                    {
                        capacityBool = true;
                    }
                    else
                    {
                        capacityBool = false;
                    }
                }
                else if (Filters.capacityCompMode == "=")
                {
                    if (component.Capacity == Filters.capacity)
                    {
                        capacityBool = true;
                    }
                    else
                    {
                        capacityBool = false;
                    }
                }
                else
                {
                    capacityBool = false;
                }
            }
            else
            {
                capacityBool = true;
            }

            if (Filters.filterFavorite)
            {
                if (Filters.favorite == component.Favorited)
                {
                    favoriteBool = true;
                }
                else if (Filters.favorite != component.Favorited)
                {
                    favoriteBool = false;
                }
            }
            else
            {
                favoriteBool = true;
            }

            return searchBool && typeBool && manuBool && statusBool && serBool && subSerBool && type2Bool && modelBool && powerBool && countBool && capacityBool && favoriteBool;
        }
            */

        private void Filtering_Changed(object sender, RoutedEventArgs e)
        {
            inputText = Search_Box.Text;
            CheckComponentsViewForNull();
        }

        private void Hledat_click(object sender, RoutedEventArgs e)
        {
            Lookup.Visibility = Visibility.Collapsed;
            Filters.type = "all";
            CheckComponentsViewForNull();
            Search.Visibility = Visibility.Visible;
        }

        #region Click specific filtering
        private void Mb_click(object sender, RoutedEventArgs e)
        {
            Lookup.Visibility = Visibility.Collapsed;
            Filters.type = "Mb";
            CheckComponentsViewForNull();
            Search.Visibility = Visibility.Visible;
        }

        private void Cpu_click(object sender, RoutedEventArgs e)
        {
            Lookup.Visibility = Visibility.Collapsed;
            Filters.type = "CPU";
            CheckComponentsViewForNull();
            Search.Visibility = Visibility.Visible;
        }

        private void Ram_click(object sender, RoutedEventArgs e)
        {
            Lookup.Visibility = Visibility.Collapsed;
            Filters.type = "RAM";
            CheckComponentsViewForNull();
            Search.Visibility = Visibility.Visible;
        }

        private void Gpu_click(object sender, RoutedEventArgs e)
        {
            Lookup.Visibility = Visibility.Collapsed;
            Filters.type = "GPU";
            CheckComponentsViewForNull();
            Search.Visibility = Visibility.Visible;
        }

        private void Psu_click(object sender, RoutedEventArgs e)
        {
            Lookup.Visibility = Visibility.Collapsed;
            Filters.type = "PSU";
            CheckComponentsViewForNull();
            Search.Visibility = Visibility.Visible;
        }

        private void Disk_click(object sender, RoutedEventArgs e)
        {
            Lookup.Visibility = Visibility.Collapsed;
            Filters.type = "Disk";
            CheckComponentsViewForNull();
            Search.Visibility = Visibility.Visible;
        }

        private void Case_click(object sender, RoutedEventArgs e)
        {
            Lookup.Visibility = Visibility.Collapsed;
            Filters.type = "Case";
            CheckComponentsViewForNull();
            Search.Visibility = Visibility.Visible;
        }

        private void Other_click(object sender, RoutedEventArgs e)
        {
            Lookup.Visibility = Visibility.Collapsed;
            Filters.type = "Jiné";
            CheckComponentsViewForNull();
            Search.Visibility = Visibility.Visible;
        }

        #endregion

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
                    Components.Remove(komponenta);
                }
            }
        }

        
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
        
        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            Filter_window Fwindow = new Filter_window(Filters, PossibleParameters, this);

            if (Fwindow.ShowDialog() == true)
            {
                Debug.WriteLine($"Dialog returned true");
                CheckComponentsViewForNull();
            }
        }

        private void ClearF_Click(object sender, RoutedEventArgs e)
        {
            filterParameters.Clear();

            CheckComponentsViewForNull();
        }

        private void CheckComponentsViewForNull()
        {
            ComponentsView.Refresh();
            if (ComponentsView.IsEmpty)
            {
                Component_grid.Visibility = Visibility.Collapsed;
                EmptyGrid_Text.Visibility = Visibility.Visible;
            }
            else
            {
                Component_grid.Visibility = Visibility.Visible;
                EmptyGrid_Text.Visibility = Visibility.Collapsed;
            }
        }

        private void Favorite_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button) sender;

            if (button.DataContext is Component komponenta && komponenta.Favorited == false)
            {
                komponenta.Favorited = true;
                ChangeButtonBg(button, komponenta.favoriteIcon);
            }
            else if (button.DataContext is Component komponentaa && komponentaa.Favorited == true)
            {
                komponentaa.Favorited = false;
                ChangeButtonBg(button, komponentaa.favoriteIcon);
            }
        }

        private void ChangeButtonBg(Button button, string path)
        {
            button.Background = new ImageBrush(new BitmapImage(new Uri(path)));
        }
    }
}