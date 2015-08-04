using MvvmTutorial.Model;
using System;
namespace MvvmTutorial.ViewModel
{
    public class StudentViewModel
    {
        #region Fields
        private readonly Student _student;
        private static object[] _genderOptions;
        #endregion // Fields
        #region Constructors
        public StudentViewModel()
        {
            Student newStudent = Student.CreateNewStudent(string.Empty, 18, Gender.Male, string.Empty);
            _student = newStudent;
        }
        public StudentViewModel(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException("student");
            }
            _student = student;
        }
        #endregion // Constructors
        #region Properties
        public string Name
        {
            get
            {
                return _student.Name;
            }
            set
            {
                _student.Name = value;
            }
        }
        public int Age
        {
            get
            {
                return _student.Age;
            }
            set
            {
                _student.Age = value;
                // Yes, we don't have validation here, so you can create a student whose age is over 1000!
            }
        }
        public Gender Gender
        {
            get
            {
                return _student.Gender;
            }
            set
            {
                _student.Gender = value;
            }
        }
        public string Description
        {
            get
            {
                return _student.Description;
            }
            set
            {
                _student.Description = value;
            }
        }
        public object[] GenderOptions
        {
            get
            {
                if (_genderOptions == null)
                {
                    _genderOptions = new object[]
                    {
                        new { DisplayMember = Gender.Male.ToString(), ValueMember = Gender.Male },
                        new { DisplayMember = Gender.Female.ToString(), ValueMember = Gender.Female }
                    };
                }
                return _genderOptions;
            }
        }
        #endregion // Properties
        #region Public Methods
        public void Save()
        {
            DataRepository.GetInstance().SaveStudent(_student);
            _student.IsNew = false;
        }
        #endregion // Public Methods
    }
}
