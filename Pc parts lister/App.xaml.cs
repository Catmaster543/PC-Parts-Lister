using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Pc_parts_lister
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SetLanguage();

            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void SetLanguage()
        {
            string language = global::Pc_parts_lister.Properties.Settings.Default.Language;

            var culture = new CultureInfo(language);
            CultureInfo.CurrentUICulture = culture;
            CultureInfo.CurrentCulture = culture;
        }
    }
}
