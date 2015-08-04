using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
namespace MvvmTutorial.Model
{
    public class DataRepository
    {
        #region Fields
        private static DataRepository _instance;
        private DataSet _data;
        /// <summary>
        /// The file path that we use to store XML file which holds all the app data we need
        /// </summary>
        public static readonly string PhysicalFilePath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "mvvm.dat");
        /// <summary>
        /// Name of [Students] table
        /// </summary>
        private const string StudentTableName = "Students";
        #endregion // Fields
        #region Constructors
        public static DataRepository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataRepository();
            }
            return _instance;
        }
        private DataRepository()
        {
            this.CreateDataSource();
            if (!File.Exists(PhysicalFilePath))
            {
                this.AddDefaultStudents();
                this.Save();
            }
            else
            {
                this.Load();
            }
        }
        #endregion // Constructors
        #region Properties
        /// <summary>
        /// Gets Students table
        /// </summary>
        private DataTable Students
        {
            get
            {
                return _data.Tables[StudentTableName];
            }
        }
        #endregion // Properties
        #region Public Methods
        /// <summary>
        /// Gets all the rows from Students table, transforms them into Student and returns a list of Student intances.
        /// </summary>
        public IEnumerable<Student> GetStudents()
        {
            foreach (DataRow dataRow in Students.Rows)
            {
                yield return Student.FromDataRow(dataRow);
            }
        }
        public void SaveStudent(Student student)
        {
            // If current student is a new record, it means this student is a new tuple, so we
            // need to add it into table
            if (student.IsNew)
            {
                AddStudent(student);
            }
            else
            {
                // Otherwise, the student may have existed, we need to find it and update its values to current values
                // Here, we use LINQ to search the student.
                var target = Students.AsEnumerable().FirstOrDefault(p => p.Field<Guid>(Student.StudentIDPropertyName) == student.StudentID);
                if (target != null) // We indeed found our wanted student record, so update it
                {
                    target.SetField<string>(Student.NamePropertyName, student.Name);
                    target.SetField<int>(Student.AgePropertyName, student.Age);
                    target.SetField<Gender>(Student.GenderPropertyName, student.Gender);
                    target.SetField<string>(Student.DescriptionPropertyName, student.Description);
                }
            }
            _data.AcceptChanges();
            this.Save();
        }
        #endregion // Public Methods
        #region Private Methods
        /// <summary>
        /// Creates the structure of all tables
        /// </summary>
        private void CreateDataSource()
        {
            _data = new DataSet("StudentData");
            DataTable students = new DataTable(StudentTableName);
            students.Columns.Add(Student.StudentIDPropertyName, typeof(Guid));
            students.Columns.Add(Student.NamePropertyName, typeof(string));
            students.Columns.Add(Student.AgePropertyName, typeof(int));
            students.Columns.Add(Student.GenderPropertyName, typeof(Gender));
            students.Columns.Add(Student.DescriptionPropertyName, typeof(string));
            _data.Tables.Add(students);
        }
        /// <summary>
        /// Adds some default student rows to Students table if there aren't any yet.
        /// </summary>
        private void AddDefaultStudents()
        {
            int total = new Random(Guid.NewGuid().GetHashCode()).Next(10, 30);
            for (int i = 0; i < total; ++i)
            {
                string randomName = "Name " + i;
                int randomAge = new Random(Guid.NewGuid().GetHashCode()).Next(17, 30);
                Gender randomGender = i % 2 == 0 ? Gender.Male : Gender.Female;
                string randomDescription = "Description " + i;
                Student randomStudent = Student.CreateNewStudent(randomName, randomAge, randomGender, randomDescription);
                this.AddStudent(randomStudent);
            }
        }
        /// <summary>
        /// Accepts all changes to current repository and persist changes to XML file
        /// </summary>
        private void Save()
        {
            _data.AcceptChanges();
            _data.WriteXml(PhysicalFilePath);
        }
        /// <summary>
        /// Reads data from XML file
        /// </summary>
        private void Load()
        {
            _data.ReadXml(PhysicalFilePath);
        }
        /// <summary>
        /// Adds one student as a new row to Students table
        /// </summary>
        /// <param name="student"></param>
        private void AddStudent(Student student)
        {
            Students.Rows.Add(new object[]
                {
                    student.StudentID,
                    student.Name,
                    student.Age,
                    student.Gender,
                    student.Description
                });
        }
        #endregion // Private Methods
    }
}
