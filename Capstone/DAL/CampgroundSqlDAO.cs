using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class CampgroundSqlDAO : ICampgroundSqlDAO
    {
        private string connectionString;

        public CampgroundSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public IList<Campground> GetCampgroundsByParkId(int park_id) // TODO: Reference Assign EmployeeToProject ProjectCLI
        {
            // Declare the result variable
            List<Campground> list = new List<Campground>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    // Create the command for sql statement
                    string sql = "Select * From campground Where park_id = @park_id Order By name";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@park_id", park_id);

                    // Execute the query and get the result set in a reader
                    SqlDataReader rdr = cmd.ExecuteReader();

                    // For each row, create a new department and add it to the list
                    while (rdr.Read())
                    {
                        Campground campground = RowToObject(rdr);

                        list.Add(campground);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occured reading campgrounds by park.");
                Console.WriteLine(ex.Message);
                throw;
            }

            return list;
        }

        public decimal GetCampgroundCost(string campground_id)
        {
            // Declare the result variable
            decimal campgroundCost = 0.00M;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    // Create the command for sql statement
                    string sql = "Select daily_fee From campground Where campground_id = @campground_id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@campground_id", campground_id);

                    // Execute the query and get the result set in a reader
                    SqlDataReader rdr = cmd.ExecuteReader();

                    // For each row, create a new department and add it to the list
                    while (rdr.Read())
                    {
                        campgroundCost = Convert.ToDecimal(rdr["daily_fee"]);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return campgroundCost;
        }
        private static Campground RowToObject(SqlDataReader rdr)
        {
            Campground campground = new Campground();
            campground.CampgroundId = Convert.ToInt32(rdr["campground_id"]);
            campground.ParkId = Convert.ToInt32(rdr["park_id"]);
            campground.Name = Convert.ToString(rdr["name"]);
            campground.OpenFromMonth = Convert.ToInt32(rdr["open_from_mm"]);
            campground.OpenToMonth = Convert.ToInt32(rdr["open_to_mm"]);
            campground.DailyFee = Convert.ToDecimal(rdr["daily_fee"]);
            return campground;
        }
    }
}
