using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Logger_Project
{
    static public class Logger
    {
        static string connectionstring;
        static Logger()
        {
            try
            {
                connectionstring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            }
            catch(Exception ex)
            {
                throw new Exception("Something went wrong while getting the DefaultConnectionString for Logger");
            }
        }
        public static void Log(Exception ex)
        {
            try
            {
                using (SqlConnection _con = new SqlConnection(connectionstring))
                {
                    _con.Open();
                    using (SqlCommand com = _con.CreateCommand())
                    {
                        com.CommandText = "InsertLogItem";
                        com.CommandType = System.Data.CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@message", ex.Message);
                        com.Parameters.AddWithValue("@stacktrace", ex.StackTrace.ToString());
                        com.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception Ex)
            {
                string p = HttpContext.Current.Server.MapPath("~");
                p += @"ErrorLog.Log";
                System.IO.File.AppendAllText(p, "While trying to record the exception to the DataBase, an exception occured\r\n");
                System.IO.File.AppendAllText(p, Ex.ToString());
                System.IO.File.AppendAllText(p, "This is the exception that was attempting to be written\r\n");
                System.IO.File.AppendAllText(p, Ex.ToString());
            }
        }
    }
    
}
