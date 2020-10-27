using Microsoft.VisualStudio.TestTools.UnitTesting;
using JobSityChallenge.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSityChallenge.Bussiness.Tests
{
    [TestClass()]
    public class ChatRoomBussinessTests
    {
        ChatRoomBussiness bo;
        [TestMethod()]
        public void PostMessageTest()
        {
            bo = new ChatRoomBussiness();
            bo._userId = "1";
            bo._message = "Mensaje de prueba";
            int rows = bo.PostMessage();
            Assert.IsTrue(rows > 0);
        }

        [TestMethod()]
        public void PostMessageBotTest()
        {
            bo = new ChatRoomBussiness();
            bo._userId = "1";
            bo._message = "/stock=stock_code";
            int rows = bo.PostMessage();
            Assert.IsTrue(rows > 0);
        }

       
    }
}