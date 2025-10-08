using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaskEnTidLib.Models;


namespace VaskEnTidLib.Repositories
{
    public class BookingRepo
    {
        private readonly string _connectionString;

        public BookingRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Booking> GetBookingsByUserId(int userId)
        {
            var bookings = new List<Booking>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SelectBookingsByUserID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var booking = new Booking
                        {
                            BookingID = reader.GetInt32(reader.GetOrdinal("BookingID")),
                            MachineID = reader.GetInt32(reader.GetOrdinal("MachineID")),
                            UserID = userId,
                            Date = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("BookingDate"))),
                            StartTime = TimeOnly.FromTimeSpan(reader.GetTimeSpan(reader.GetOrdinal("StartTime"))),
                            EndTime = TimeOnly.FromTimeSpan(reader.GetTimeSpan(reader.GetOrdinal("EndTime")))
                        };
                        bookings.Add(booking);
                    }
                }


            return bookings;
            }
        }
    }
}
