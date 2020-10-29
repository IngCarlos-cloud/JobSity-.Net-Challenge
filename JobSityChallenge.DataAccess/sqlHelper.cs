using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSityChallenge.DataAccess
{
    public static class sqlHelper
    {
        public const string RegistryPath = @"Software\JobSity\DB";
        public static SqlConnection GetSqlConnection()
        {
            try
            {
                string cnnString =  BuildConnectionString();
                SqlConnection connection = new SqlConnection(cnnString);
                connection.Open();
                return connection;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static SqlDataReader ExecuteReader( string spName, List<SqlParameter> sqlParam)
        {
            SqlConnection connection = GetSqlConnection();
                if (connection == null)
                    throw new Exception($"Unable to connect to DB");

            SqlCommand command = new SqlCommand(spName);
            command.Connection = connection;
            command.CommandType = System.Data.CommandType.StoredProcedure;

            if (sqlParam != null)
                command.Parameters.AddRange(sqlParam.ToArray());


            return command.ExecuteReader();
        }

        public static DataSet ExecuteDataSet(string spName, List<SqlParameter> sqlParam)
        {
            SqlConnection connection = GetSqlConnection();
            if (connection == null)
                throw new Exception($"Unable to connect to DB");

            SqlCommand command = new SqlCommand(spName);
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();

            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;

            if (sqlParam != null)
                command.Parameters.AddRange(sqlParam.ToArray());
            adapter.SelectCommand = command;

            adapter.Fill(dataSet);
            return dataSet;


        }

        public static int ExecuteNonQuery(string spName, List<SqlParameter> sqlParam)
        {
            SqlConnection connection = GetSqlConnection();
            if (connection == null)
                throw new Exception($"Unable to connect to DB");

            SqlCommand command = new SqlCommand(spName);                        
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;

            if (sqlParam != null)
                command.Parameters.AddRange(sqlParam.ToArray());

            return command.ExecuteNonQuery();


        }

        public static int ExecuteEscalar(string spName, List<SqlParameter> sqlParam)
        {
            SqlConnection connection = GetSqlConnection();
            if (connection == null)
                throw new Exception($"Unable to connect to DB");

            SqlCommand command = new SqlCommand(spName);
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;

            if (sqlParam != null)
                command.Parameters.AddRange(sqlParam.ToArray());

            var result = command.ExecuteScalar();

            return int.Parse(result.ToString());


        }

        private static string BuildConnectionString()
        {
            string cnnString = string.Empty;
            using (RegistryKey dbKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser,RegistryView.Registry64).OpenSubKey(RegistryPath))
            {
                if (dbKey != null)
                {
                    //"Data Source = (localdb)\\ProjectsV13; Initial Catalog = JobSityDb; User ID = JobSityU; Password = 'l{xqafvdkFbNpogkybYm{|s4msFT7_&#$!~<t{pqcbZMp9wk'; Integrated Security=True;Pooling=False;Connect Timeout=30";
                    cnnString = $"Data Source={dbKey.GetValue("Server")};Initial Catalog={dbKey.GetValue("dbName")}; User ID={dbKey.GetValue("user")}; Password='{dbKey.GetValue("pwd")}'; Integrated Security=True;Pooling=False;Connect Timeout=30";
                    
                }
                    
            }
            return cnnString;
        }
    }
}
