using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace app_login
{
    public class helper
    {
        private SqlConnection _connection;
        private static helper _Instance;
        public static helper Instance
        {
            get
            {
                if (_Instance == null)
                {
                    string query = @"Data Source=DESKTOP-IPDB6V8\SQLEXPRESS2022;Initial Catalog=app_login;Integrated Security=True;TrustServerCertificate=True";
                    _Instance = new helper(query);
                }
                return _Instance;
            }
            set { }
        }
        public helper(String s)
        {
            _connection = new SqlConnection(s);
        }
        public DataTable getRecords(String query, SqlParameter[] parameters = null)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(query, _connection);
            DataTable result = new DataTable();
            if (parameters != null)
            {
                adapter.SelectCommand.Parameters.AddRange(parameters);
            }
            try
            {
                _connection.Open();
                adapter.Fill(result);
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }
        public int excuteDB(string query, SqlParameter[] parameters = null)
        {
            int result = 0;
            SqlCommand command = new SqlCommand(query, _connection);
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            try
            {
                _connection.Open();
                result = command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }

    }
}