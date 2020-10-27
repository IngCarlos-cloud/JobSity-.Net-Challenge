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
        public void InsertMessage()
        {
            Chat chat = new Chat();

            int Return =  chat.InsertMessage("1", "Message for testing");
            
                Assert.IsTrue(Return > 0);
            
        }

        //[TestMethod]
        //public void RetrieveMessage()
        //{
        //    chat chat = new chat();

        //    string msgs = chat.RetriveMessages();

        //    Assert.IsTrue(!String.IsNullOrEmpty(msgs));
        //}
    }
}
