using Pc_parts_lister.Resources;
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
    /// Interakční logika pro Settings_window.xaml
    /// </summary>
    public partial class Settings_window : Window
    {
        public bool firstRun = true;
        public Settings_window()
        {
            InitializeComponent();

            foreach (ComboBoxItem item in LanguageComboBox.Items)
            {
                if (item.Tag.ToString() == Properties.Settings.Default.Language)
                {
                    LanguageComboBox.SelectedItem = item;
                }
            }
            firstRun = false;
        }

        public void LangSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!firstRun)
            {
                ComboBoxItem item = (ComboBoxItem)LanguageComboBox.SelectedItem;

                string language = item.Tag.ToString();
                Properties.Settings.Default.Language = language;
                Properties.Settings.Default.Save();

                MessageBox.Show(Strings.RestartRequest, Strings.Info, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
