using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DAL
{
    public class ParkSqlDAO : IParkSqlDAO
    {
        private string connectionString;

        public ParkSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public IList<Park> GetParks()
        {
            // Declare the result variable
            List<Park> list = new List<Park>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    // Create the command for sql statement
                    string sql = "Select * From park";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    // Execute the query and get the result set in a reader
                    SqlDataReader rdr = cmd.ExecuteReader();

                    // For each row, create a new department and add it to the list
                    while (rdr.Read())
                    {
                        Park park = RowToObject(rdr);

                        list.Add(park);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occured when getting parks.");
                Console.WriteLine(ex.Message);
            }

            return list;
        }

        private static Park RowToObject(SqlDataReader rdr)
        {
            Park park = new Park();
            park.ParkId = Convert.ToInt32(rdr["park_id"]);
            park.Name = Convert.ToString(rdr["name"]);
            park.Location = Convert.ToString(rdr["location"]);
            park.EstablishDate = Convert.ToDateTime(rdr["establish_date"]);
            park.Area = Convert.ToInt32(rdr["area"]);
            park.Visitors = Convert.ToInt32(rdr["visitors"]);
            park.Description = Convert.ToString(rdr["description"]);
            return park;
        }
    }
}
