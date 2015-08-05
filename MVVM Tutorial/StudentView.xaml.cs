using System.Windows;
using MvvmTutorial.ViewModel;
namespace MvvmTutorial
{
    /// <summary>
    /// Interaction logic for StudentView.xaml
    /// </summary>
    public partial class StudentView : Window
    {
        #region Constructors
        public StudentView()
        {
            InitializeComponent();
        }
        #endregion // Constructors
        #region Properties
        private StudentViewModel StudentVM
        {
            get
            {
                return this.DataContext as StudentViewModel;
            }
        }
        #endregion // Properties
        #region Event Handlers
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (BindingGroup.CommitEdit())
            {
                StudentVM.Save();
                this.DialogResult = true;
                this.Close();
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion // Event Handlers
    }
}
