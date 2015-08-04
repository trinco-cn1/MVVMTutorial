using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using MvvmTutorial.ViewModel;
namespace MvvmTutorial
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Fields
        private MainViewModel _mainVm;
        #endregion // Fields
        #region Protected Methods
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            // Set data context
            _mainVm = new MainViewModel();
            mainWindow.DataContext = _mainVm;
            mainWindow.Show();
        }
        #endregion // Protected Methods
    }
}
