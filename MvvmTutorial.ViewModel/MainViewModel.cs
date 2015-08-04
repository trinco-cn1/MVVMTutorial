using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmTutorial.Model;
using System.Collections.ObjectModel;
namespace MvvmTutorial.ViewModel
{
    public class MainViewModel
    {
        #region Fields
        private ObservableCollection<StudentViewModel> _all;
        #endregion // Fields
        #region Properties
        public ObservableCollection<StudentViewModel> All
        {
            get
            {
                if (_all == null)
                {
                    _all = new ObservableCollection<StudentViewModel>();
                    foreach (var student in DataRepository.GetInstance().GetStudents())
                    {
                        _all.Add(new StudentViewModel(student));
                    }
                }
                return _all;
            }
        }
        #endregion // Properties
    }
}
