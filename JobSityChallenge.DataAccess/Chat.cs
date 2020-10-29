using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSityChallenge.DataAccess
{
    public class Chat
    {

        public int InsertMessage(string userId, string message)
        {

            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter(){ ParameterName="@UserId",Value=userId},
                new SqlParameter(){ ParameterName="@Message",Value=message}
            };

                return sqlHelper.ExecuteNonQuery("InsertMessage",parameters);
            }
            catch (Exception)
            {
                return 0;
            }
            
        }

        public DataSet RetrieveMessages()
        {

            try
            {
               return sqlHelper.ExecuteDataSet("RetrieveMessages",null);
            }
            catch (Exception e)
            {
                return null;
            }

        }

    }
}
