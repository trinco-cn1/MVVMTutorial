using MvvmTutorial.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
namespace MvvmTutorial.Test.Model
{
    /// <summary>
    ///This is a test class for DataRepositoryTest and is intended
    ///to contain all DataRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DataRepositoryTest
    {

        private TestContext testContextInstance;
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for GetStudents
        ///</summary>
        [TestMethod()]
        public void GetStudentsTest()
        {
            DataRepository target = DataRepository.GetInstance();
            Debug.WriteLine(DataRepository.PhysicalFilePath);
            foreach (Student student in target.GetStudents())
            {
                Debug.WriteLine(student.Name);
            }
        }
        /// <summary>
        ///A test for persisting a new student by using SaveStudent method
        ///</summary>
        [TestMethod()]
        public void AddNewStudentTest()
        {
            DataRepository target = DataRepository.GetInstance();
            var expect = Student.CreateNewStudent("Chris", 20, Gender.Male, "Lazy...");
            target.SaveStudent(expect);
            target = DataRepository.GetInstance();
            var actual = target.GetStudents().FirstOrDefault(p => p.StudentID == expect.StudentID);
            if (actual != null)
            {
                Assert.AreEqual(actual.StudentID, expect.StudentID);
                Assert.AreEqual(actual.Name, actual.Name);
                Assert.AreEqual(actual.Age, expect.Age);
                Assert.AreEqual(actual.Gender, expect.Gender);
                Assert.AreEqual(actual.Description, actual.Description);
            }
            else
            {
                Assert.Fail("Didn't find the persisted student");
            }
        }
        /// <summary>
        /// A test for updating an existing student by using SaveStudent method
        /// </summary>
        [TestMethod()]
        public void UpdateStudentTest()
        {
            DataRepository target = DataRepository.GetInstance();
            var expect = target.GetStudents().First();
            expect.Name = "John";
            expect.Gender = Gender.Male;
            expect.Age = 18;
            expect.Description = "Hello world!";
            target.SaveStudent(expect);
            target = DataRepository.GetInstance();
            var actual = target.GetStudents().FirstOrDefault(p => p.StudentID == expect.StudentID);
            if (actual != null)
            {
                Assert.AreEqual(actual.StudentID, expect.StudentID);
                Assert.AreEqual(actual.Name, actual.Name);
                Assert.AreEqual(actual.Age, expect.Age);
                Assert.AreEqual(actual.Gender, expect.Gender);
                Assert.AreEqual(actual.Description, actual.Description);
            }
            else
            {
                Assert.Fail("Didn't find the updated student");
            }
        }
    }
}
