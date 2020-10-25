using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobSityChallenge.Models
{
    public class ChatRoomModel
    {
        
        public class Messages
        {
            public string userId { get; set; }
            public string UserName { get; set; }
            public string Message { get; set; }
            public string CreateDate { get; set; }

        }
    }
}