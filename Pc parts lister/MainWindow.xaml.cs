using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Pc_parts_lister
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Component> Components { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Components = new ObservableCollection<Component>();
            Components = DataStorage.Load();
            DataContext = this;
        }

        private void Pridat(object sender, RoutedEventArgs e)
        {
            var window = new Add_part();

            if (window.ShowDialog() == true)
            {
                Components.Add(window.Component);
            }
        }

        private void Hledat(object sender, RoutedEventArgs e)
        {
            Lookup_window window = new Lookup_window(Components);
            //window.DataContext = Components;
            window.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DataStorage.Save(Components);
        }

        
        /* private void DeleteButton_Click(object sender, RoutedEventArgs e)
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
                    Components.Remove(komponenta);
                }
            }
        }
        */

        /*private void DetailButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button &&
        button.DataContext is Component komponenta)
            {
                DetailWindow Dwindow = new DetailWindow(komponenta);
                Dwindow.ShowDialog();
            }
        }
        */

        /*private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button &&
        button.DataContext is Component komponenta)
            {
                Edit_window Ewindow = new Edit_window(komponenta);
                Ewindow.ShowDialog();
            }
        }
        */
    }

    public class Component
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Manufacturer { get; set; }
        public string Series { get; set; }
        public string SubSeries { get; set; }
        public string Model { get; set; }
        public string Space { get; set; }
        public string SubType { get; set; }
        public string Power { get; set; }
        public string Quantity {  get; set; }

        private string imagePath;
        public string ImagePath
        {
            get => imagePath;
            set
            {
                imagePath = value;
                OnPropertyChanged(); 
                OnPropertyChanged(nameof(FullImagePath));
            }
        }

        public string FullImagePath
        {
            get
            {
                if (string.IsNullOrEmpty(ImagePath))
                    return null;

                return Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    ImagePath);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public static class DataStorage
    {
        private static string FolderPath =
            System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "PcInventory");

        private static string FilePath =
            System.IO.Path.Combine(FolderPath, "components.json");

        public static void Save(IEnumerable<Component> components)
        {
            Directory.CreateDirectory(FolderPath);

            var json = JsonSerializer.Serialize(components, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(FilePath, json);
        }

        public static ObservableCollection<Component> Load()
        {
            if (!File.Exists(FilePath))
                return new ObservableCollection<Component>();

            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<ObservableCollection<Component>>(json)
                   ?? new ObservableCollection<Component>();
        }
    }
}
