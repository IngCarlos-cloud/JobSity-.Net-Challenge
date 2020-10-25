using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSityChallenge.DataAccess;
using Newtonsoft.Json;

namespace JobSityChallenge.Bussiness
{
    public class ChatRoomBussiness
    {

        public string _userId { get; set; }                
        public string _message { get; set; }


        public int PostMessage()
        {
            Chat chat = new Chat();
            int  rowsaffected= 0;

            try
            {
                rowsaffected = chat.InsertMessage(_userId, _message);                             
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

    }
}
