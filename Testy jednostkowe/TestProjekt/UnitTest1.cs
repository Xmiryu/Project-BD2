using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Data.SqlClient;

namespace TestProjekt
{
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMedian()
        {
            string sqlconnection = @"SERVER=MSSQLServer; " +
             "INITIAL CATALOG=projekt; INTEGRATED SECURITY=SSPI;";
            string sqlcommand = "SELECT [dbo].[Temperature.Median](temp) FROM pogoda";
            SqlConnection connection = null;
            SqlCommand command = null;
            
            connection = new SqlConnection(sqlconnection);
            connection.Open();
            command = new SqlCommand(sqlcommand, connection);
            Object count = command.ExecuteScalar();
            Assert.AreEqual(12.5, count);
        }
        [TestMethod]
        public void TestNegatives()
        {
            string sqlconnection = @"SERVER=MSSQLServer; " +
             "INITIAL CATALOG=projekt; INTEGRATED SECURITY=SSPI;";
            string sqlcommand = "SELECT [dbo].[Temperature.uda_CountOfNegatives](temp) FROM pogoda";
            SqlConnection connection = null;
            SqlCommand command = null;

            connection = new SqlConnection(sqlconnection);
            connection.Open();
            command = new SqlCommand(sqlcommand, connection);
            Object count = command.ExecuteScalar();
            Assert.AreEqual(32, count);
        }
        [TestMethod]
        public void TestVegetation()
        {
            string sqlconnection = @"SERVER=MSSQLServer; " +
             "INITIAL CATALOG=projekt; INTEGRATED SECURITY=SSPI;";
            string sqlcommand = "SELECT [dbo].[Temperature.uda_CountOfVegetation](temp) FROM pogoda";
            SqlConnection connection = null;
            SqlCommand command = null;

            connection = new SqlConnection(sqlconnection);
            connection.Open();
            command = new SqlCommand(sqlcommand, connection);
            Object count = command.ExecuteScalar();
            Assert.AreEqual(62, count);
        }
        [TestMethod]
        public void TestDominanta()
        {
            string sqlconnection = @"SERVER=MSSQLServer; " +
             "INITIAL CATALOG=projekt; INTEGRATED SECURITY=SSPI;";
            string sqlcommand = "SELECT [dbo].[Dominanta.Dominanta](temp) FROM pogoda";
            SqlConnection connection = null;
            SqlCommand command = null;

            connection = new SqlConnection(sqlconnection);
            connection.Open();
            command = new SqlCommand(sqlcommand, connection);
            Object count = command.ExecuteScalar();
            Assert.AreEqual(-16.0, count);
        }
        [TestMethod]
        public void TestCountRange()
        {
            string sqlconnection = @"SERVER=MSSQLServer; " +
             "INITIAL CATALOG=projekt; INTEGRATED SECURITY=SSPI;";
            string sqlcommand = "SELECT [dbo].[Temperature.uda_CountOfRange](temp, 0, 24) FROM pogoda";
            SqlConnection connection = null;
            SqlCommand command = null;

            connection = new SqlConnection(sqlconnection);
            connection.Open();
            command = new SqlCommand(sqlcommand, connection);
            Object count = command.ExecuteScalar();
            Assert.AreEqual(37, count);
        }
    }
}
