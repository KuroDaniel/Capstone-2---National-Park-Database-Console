using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class ReservationSqlDAO : IReservationSqlDAO
    {
        private string connectionString;

        public ReservationSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public IList<Reservation> Search(int siteId, DateTime fromDate, DateTime endDate)
        {
            List<Reservation> list = new List<Reservation>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "Select * From campground Where site_id = @site_id AND from_date = @from_date AND to_date = @to_date";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@site_id", siteId);
                    cmd.Parameters.AddWithValue("@from_date", fromDate);
                    cmd.Parameters.AddWithValue("@to_date", endDate);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Reservation reservation = RowToObject(rdr);

                        list.Add(reservation);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occured searching for the reservation.");
                Console.WriteLine(ex.Message);
            }

            return list;
        }

        public int CreateReservation(string site_id, string name, string from_date, string to_date)
        {
            int newId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql =
@" Insert into reservation
(site_id, name, from_date, to_date, create_date)
values (@site_id, @name, @from_date, @to_date, @create_date);
Select @@identity;
";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@site_id", site_id);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@from_date", from_date);
                    cmd.Parameters.AddWithValue("@to_date", to_date);
                    cmd.Parameters.AddWithValue("@create_date", DateTime.Now);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("Select max(reservation_id) as reservation_id From reservation", conn);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        newId = Convert.ToInt32(rdr["reservation_id"]);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return newId;
        }

        private static Reservation RowToObject(SqlDataReader rdr)
        {
            Reservation reservation = new Reservation();
            reservation.ReservationId = Convert.ToInt32(rdr["reservation_id"]);
            reservation.SiteId = Convert.ToInt32(rdr["site_id"]);
            reservation.Name = Convert.ToString(rdr["name"]);
            reservation.FromDate = Convert.ToDateTime(rdr["from_date"]);
            reservation.ToDate = Convert.ToDateTime(rdr["to_date"]);
            reservation.CreateDate = Convert.ToDateTime(rdr["create_date"]);
            return reservation;
        }
    }
}

