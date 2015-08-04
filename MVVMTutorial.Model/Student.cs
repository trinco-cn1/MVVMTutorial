using System;
using System.Data;
namespace MvvmTutorial.Model
{
    public class Student
    {
        #region Fields
        public const string StudentIDPropertyName = "StudentID";
        public const string NamePropertyName = "Name";
        public const string AgePropertyName = "Age";
        public const string GenderPropertyName = "Gender";
        public const string DescriptionPropertyName = "Description";
        #endregion // Fields
        #region Constructors
        /// <summary>
        /// 由DataRow对象建立一个Ｓｔｕｄｅｎｔ实例
        /// </summary>
        public static Student FromDataRow(DataRow dataRow)
        {
            return new Student()
            {
                StudentID = dataRow.Field<Guid>(Student.StudentIDPropertyName),
                Name = dataRow.Field<string>(Student.NamePropertyName),
                Age = dataRow.Field<int>(Student.AgePropertyName),
                Gender = dataRow.Field<Gender>(Student.GenderPropertyName),
                Description = dataRow.Field<string>(Student.DescriptionPropertyName)
            };
        }
        /// <summary>
        /// 建立一个新Ｓｔｕｄｅｎｔ的实例
        /// </summary>
        public static Student CreateNewStudent(string name, int age, Gender gender, string description)
        {
            return new Student()
            {
                IsNew = true,
                StudentID = Guid.NewGuid(),
                Name = name,
                Age = age,
                Gender = Gender.Male,
                Description = description
            };
        }
        private Student() { }
        #endregion // Constructors
        #region Properties
        public Guid StudentID
        {
            get;
            private set;
        }
        public string Name
        {
            get;
            set;
        }
        public int Age
        {
            get;
            set;
        }
        public Gender Gender
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }

        // 用于表示当前Ｓｔｕｄｅｎｔ对象是否是新加入的（即还未被保存）
        public bool IsNew
        {
            get;
            set;
        }
        #endregion // Properties
    }
    public enum Gender
    {
        Male,
        Female
    }
}
