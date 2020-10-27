using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using JobSityChallenge.DataAccess;
using JobSityChallenge.Bot;
using Newtonsoft.Json;
using System.Net.Configuration;
using Newtonsoft.Json.Linq;

namespace JobSityChallenge.Bussiness
{
    public class ChatRoomBussiness
    {

        public string _userId { get; set; }                
        public string _message { get; set; }

        public bool isCommand { get; set; }
        
        public string bootMessage { get; set; }

        public int PostMessage()
        {
            int rowsaffected = 0;
            try
            {                
                evaluateMessage(_message);
                if (isCommand)
                {

                    rowsaffected = callBrokerBot();

                }
                else
                {
                    Chat chat = new Chat();
                    rowsaffected = chat.InsertMessage(_userId, _message);
                }
            }
            catch (Exception)
            {
                rowsaffected = -1;
            }
            
                return rowsaffected;
                            
         }

        public string RetriveMessages()
        {
            Chat chatDB = new Chat();
            string messages = string.Empty;

            try 
            {                
                DataSet ds = chatDB.RetrieveMessages();

                if (ds?.Tables?.Count > 0)
                {
                    messages = JsonConvert.SerializeObject(ds.Tables[0]);
                }
            }
            catch (Exception) { messages = "Unable to retrieve messages"; }
            return messages;
        }

        private int callBrokerBot()
        {
            
            BrokerBot bot = new BrokerBot();
            Messages botMessage;
            string stockCode = _message.Substring(_message.IndexOf("=")+1).Trim();
            string jsonResponse = bot.getStockQuote(stockCode);
            var jsontxt = JToken.Parse(jsonResponse);

            bool result;
            bool.TryParse(((JValue)jsontxt["Result"]).ToString(), out result);
            
            botMessage = new Messages();
                botMessage.CreateDate = DateTime.Now.ToString();                
                botMessage.UserName = "RabbitMQ";

            if (result)
                botMessage.Message = $" {stockCode.ToUpper()} quote is {((JValue)jsontxt["message"])?.ToString()} per share";
            else
                botMessage.Message = ((JValue)jsontxt["message"])?.ToString();

            bootMessage = JsonConvert.SerializeObject(botMessage, Formatting.Indented);
            return 1;
        }
        private void evaluateMessage(string msg)
        {
            
            if (msg.Contains("/stock="))
            {
                isCommand = msg.Substring(msg.IndexOf("=")+1).Trim().Length > 0 ? true : false;
            }
        }


        private struct Messages
        {
            public string userId { get; set; }
            public string UserName { get; set; }
            public string Message { get; set; }
            public string CreateDate { get; set; }

        }
    }
}
