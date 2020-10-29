using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSityChallenge.DataAccess;

namespace JobSityChallenge.DataAccess
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }

        public bool AuthenticateUSer()
        {
            bool validUser = false;

            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter(){ ParameterName="@userName",Value=UserName},
                new SqlParameter(){ ParameterName="@password",Value=Password}
            };
                SqlDataReader userRd = sqlHelper.ExecuteReader("ValidateUser", parameters);
                if (userRd.Read())
                {                    
                    UserId = userRd.GetFieldValue<int>(0);
                    validUser = true;
                }
                else
                    validUser = false;

                return validUser;
            }
            catch (Exception e)
            {
                return validUser;
            }
        }

        public bool CreateUser()
        {
            
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter(){ ParameterName="@userName",Value=UserName},
                new SqlParameter(){ ParameterName="@userPwd",Value=Password}
            };
                UserId = sqlHelper.ExecuteEscalar("InsertUser", parameters);
                bool userCreated = UserId > 0;
                return userCreated;
            }
            catch (Exception e)
            {                
            }return false;
        }
    }
}
