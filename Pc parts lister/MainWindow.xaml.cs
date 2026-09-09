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

        public ObservableCollection<PossibleParameter> PossibleParameters { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Components = new ObservableCollection<Component>();
            Components = DataStorage.LoadComponents();

            PossibleParameters = new ObservableCollection<PossibleParameter>();
            PossibleParameters = DataStorage.LoadParameters();

            DataContext = this;
        }

        private void Pridat(object sender, RoutedEventArgs e)
        {
            var window = new Add_part(PossibleParameters);

            if (window.ShowDialog() == true)
            {
                Components.Add(window.Komponenta);
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
            DataStorage.SaveComponents(Components);
            DataStorage.SaveParameters(PossibleParameters);
        }
    }

    public class Component
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Favorited { get; set; }
        public string Status { get; set; }

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
        public string favoriteIcon
        {
            get
            {
                if (Favorited)
                {
                    return "pack://application:,,,/favorite_button_on.png";
                }
                else
                {
                    return "pack://application:,,,/favorite_button.png";
                }
            }
        }
        public List<string> imagePaths { get; set; } = new List<string>(1);

        public List<Parameter> parameters { get; set; } = new List<Parameter>(); 
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public class Parameter
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string Value { get; set; }
        public Type type { get; set; }
        public enum Type
        {
            Number,
            String,
            Boolean
        }
    }

    public class PossibleParameter : Parameter
    {
        // Nastavení pravidel

        public List<string> values { get; set; }
        public string requiredType { get; set; }
        public List<PossibleParameter> reuiredParameters { get; set; }

        public void WriteDefaults(ref ObservableCollection<PossibleParameter> parameters)
        {
            PossibleParameter typeParameter = new PossibleParameter();
            typeParameter.Name = "Typ";
            typeParameter.ID = "Type";
            typeParameter.values = new List<string>();
            typeParameter.values.Add("CPU");
            typeParameter.values.Add("GPU");
            typeParameter.values.Add("RAM");
            typeParameter.values.Add("Mb");
            typeParameter.values.Add("Disk");
            typeParameter.values.Add("PSU");
            typeParameter.values.Add("Case");
            typeParameter.values.Add("Jiné");
            typeParameter.type = Parameter.Type.String;
            parameters.Add(typeParameter);

            PossibleParameter countParameter = new PossibleParameter();
            countParameter.Name = "Počet";
            countParameter.ID = "Count";
            countParameter.type = PossibleParameter.Type.Number;
            parameters.Add(countParameter);

            PossibleParameter manuParameter = new PossibleParameter();
            manuParameter.Name = "Výrobce";
            manuParameter.values = new List<string>();
            manuParameter.values.Add("MSI");
            manuParameter.values.Add("Gigabyte");
            manuParameter.type = Parameter.Type.String;
            parameters.Add(manuParameter);

            PossibleParameter serParameter = new PossibleParameter();
            serParameter.Name = "Série";
            serParameter.values = new List<string>();
            serParameter.values.Add("Core");
            serParameter.values.Add("Atom");
            serParameter.type = Parameter.Type.String;
            parameters.Add(serParameter);

            PossibleParameter subSerParameter = new PossibleParameter();
            subSerParameter.Name = "Subsérie";
            subSerParameter.type = Parameter.Type.String;
            parameters.Add(subSerParameter);

            PossibleParameter modelParameter = new PossibleParameter();
            modelParameter.Name = "Model";
            modelParameter.type = Parameter.Type.String;
            parameters.Add(modelParameter);

            PossibleParameter capParameter = new PossibleParameter();
            capParameter.Name = "Kapacita";
            capParameter.type = Parameter.Type.String;
            parameters.Add(capParameter);

            PossibleParameter subTypeParameter = new PossibleParameter();
            subTypeParameter.Name = "Subtyp";
            subTypeParameter.type = Parameter.Type.String;
            parameters.Add(subTypeParameter);

            PossibleParameter powerParameter = new PossibleParameter();
            powerParameter.Name = "Výkon";
            powerParameter.type = Parameter.Type.String;
            parameters.Add(powerParameter);
        }
    }

    public static class DataStorage
    {
        private static string FolderPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Pc_Parts_Lister");

        private static string ComponentFilePath = System.IO.Path.Combine(FolderPath, "components.json");
        private static string ParametersFilePath = System.IO.Path.Combine(FolderPath, "parameters.json");

        public static void SaveComponents(IEnumerable<Component> components)
        {
            Directory.CreateDirectory(FolderPath);

            var json = JsonSerializer.Serialize(components, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(ComponentFilePath, json);
        }

        public static void SaveParameters(IEnumerable<PossibleParameter> parameters)
        {
            Directory.CreateDirectory(FolderPath);

            var json = JsonSerializer.Serialize(parameters, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(ParametersFilePath, json);
        }

        public static ObservableCollection<Component> LoadComponents()
        {
            if (!File.Exists(ComponentFilePath))
            return new ObservableCollection<Component>();

            var json = File.ReadAllText(ComponentFilePath);
            return JsonSerializer.Deserialize<ObservableCollection<Component>>(json) ?? new ObservableCollection<Component>();
        }

        public static ObservableCollection<PossibleParameter> LoadParameters()
        {
            if (!File.Exists(ParametersFilePath))
            {
                PossibleParameter parameter = new PossibleParameter();
                ObservableCollection<PossibleParameter> parameters = new ObservableCollection<PossibleParameter>();
                parameter.WriteDefaults(ref parameters);
                return parameters;
            }
            

            var json = File.ReadAllText(ParametersFilePath);
            return JsonSerializer.Deserialize<ObservableCollection<PossibleParameter>>(json) ?? new ObservableCollection<PossibleParameter>();
        }
    }
}
