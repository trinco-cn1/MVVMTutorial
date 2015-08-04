using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MvvmTutorial.ViewModel;
namespace MvvmTutorial
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructors
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion // Constructors
        #region Event Handlers
        private void NewMenuItem_Click(object sender, RoutedEventArgs e)
        {
            StudentView studentView = new StudentView();
            // Set data context
            StudentViewModel newStudentVM = new StudentViewModel();
            studentView.DataContext = newStudentVM;
            studentView.ShowDialog();
            if (studentView.DialogResult == true)
            {
                (this.DataContext as MainViewModel).All.Add(newStudentVM);
            }
        }
        #endregion // Event Handlers
    }
}