using Microsoft.VisualStudio.TestTools.UnitTesting;
using JobSityChallenge.Bot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSityChallenge.Bot.Tests
{
    [TestClass()]
    public class BrokerBotTests
    {
        [TestMethod()]
        public void getQuoteTest()
        {
            BrokerBot bot = new BrokerBot();
            string quote = bot.getStockQuote("aapl.us");
            Assert.IsTrue(!string.IsNullOrEmpty(quote));
        }
    }
}