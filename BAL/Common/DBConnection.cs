using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Common
{
    public class DBConnection
    {
        public static string conString = Convert.ToString(ConfigurationManager.ConnectionStrings["DBConString"]);

        public DataTable ExecuteQuery(string query)
        {

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(conString))
                {
                    DataTable dt = new DataTable();
                    if (sqlConnection != null && sqlConnection.State == ConnectionState.Closed) 
                    {
                        sqlConnection.Open();
                    }
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    { 
                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                        DataSet dataSet = new DataSet();
                        sqlDataAdapter.Fill(dataSet);

                        dt = dataSet.Tables[0];
                    }
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool ExecuteNonQuery(string query)
        {
            bool result = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(conString))
                {
                    if (sqlConnection != null && sqlConnection.State == ConnectionState.Closed)
                    {
                        sqlConnection.Open();
                    }

                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.ExecuteNonQuery();
                        result = true;
                    }

                    if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
                    {
                        sqlConnection.Close();
                    }
                
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;
        }


    }
}
