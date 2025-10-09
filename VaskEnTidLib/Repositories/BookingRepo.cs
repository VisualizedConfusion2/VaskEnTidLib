using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaskEnTidLib.Models;
using System.Reflection.PortableExecutable;


namespace VaskEnTidLib.Repositories
{
    public class BookingRepo
    {
        private readonly string _connectionString;

        public BookingRepo(string connectionString)
        {
            _connectionString = connectionString;
        }
        public bool CreateBooking(int userId, int machineId, DateOnly date, TimeOnly startTime, TimeOnly endTime)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("usp_CreateBooking", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@MachineID", machineId);
                cmd.Parameters.AddWithValue("@BookingDate", date.ToDateTime(TimeOnly.MinValue));
                cmd.Parameters.AddWithValue("@StartTime", startTime.ToTimeSpan());
                cmd.Parameters.AddWithValue("@EndTime", endTime.ToTimeSpan());

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true; // booking oprettet succesfuldt
                }
                catch (SqlException ex)
                {
                    // Log evt. fejlbesked, fx fra RAISERROR i SQL
                    Console.WriteLine($"Fejl ved oprettelse af booking: {ex.Message}");
                    return false;
                }
            }
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

        public List<Booking> GetBookingsInDepartmentByUserId(int userId)
        {
            var bookings = new List<Booking>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SelectBookingsInDepartmentByUserID", conn))
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
                            UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                            Date = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("BookingDate"))),
                            StartTime = TimeOnly.FromTimeSpan(reader.GetTimeSpan(reader.GetOrdinal("StartTime"))),
                            EndTime = TimeOnly.FromTimeSpan(reader.GetTimeSpan(reader.GetOrdinal("EndTime")))
                        };
                        bookings.Add(booking);
                    }
                }
            }

            return bookings;
        }

    }
}
