using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JobSityChallenge.DataAccess;
using JobSityChallenge.Bussiness;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

namespace JobSityChallenge.Tests
{
    [TestClass]
    public class DataAccessTest
    {
        [TestMethod]
        public void TestConnection()
        {
            
            SqlConnection connection = sqlHelper.GetSqlConnection();

            Assert.IsNotNull(connection);
            
        }

        [TestMethod]
        public void ValidateUser()
        {
            User usr = new User();
            usr.UserName = "JobSity";
            usr.Password = "challenge2";
            bool valid = usr.AuthenticateUSer();

            Assert.IsNotNull(valid);

        }

        [TestMethod]
        public void InserUser()
        {
            User usr = new User();
            usr.UserName = "New";
            usr.Password = "challenge2";
            usr.CreateUser();
        }

        [TestMethod]
        public void InsertMessage()
        {
            Chat chat = new Chat();

            int Return =  chat.InsertMessage("1", "Message for testing");
            
                Assert.IsTrue(Return > 0);
            
        }

        [TestMethod]
        public void RetrieveMessage()
        {
            Chat chat = new Chat();

            DataSet ds = chat.RetrieveMessages();

            Assert.IsTrue(ds?.Tables[0]?.Rows.Count>0);
        }
    }
}
