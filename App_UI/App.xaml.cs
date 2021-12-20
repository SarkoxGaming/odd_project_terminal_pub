using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace App_UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// Author : nicol
    /// Creation Date : 11/29/2021 10:36:38 AM
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var lang = App_UI.Properties.Settings.Default.Language;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
            
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ApplicationView win = new ApplicationView();

            win.Show();
        }
    }
}
