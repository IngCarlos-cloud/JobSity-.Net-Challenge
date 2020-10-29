using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobSityChallenge.Models
{
    public class Login
    {
        public class User
        {
            public int Id { get; set; }
                                  
            public string UserName { get; set; }
            public string Pwd { get; set; }
        }
    }
}