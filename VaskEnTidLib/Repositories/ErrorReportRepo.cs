using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using VaskEnTidLib.Models;

namespace VaskEnTidLib.Repositories
{
    public class ErrorReportRepo
    {
        private readonly string _connectionString;

        public ErrorReportRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<ErrorReport> GetErrorReportByUserId(int userId)
        {
            var errorReports = new List<ErrorReport>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SelectErrorReportsByUserID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var errorReport = new ErrorReport
                        {
                            ErrorID = reader.GetInt32(reader.GetOrdinal("ErrorID")),
                            MachineID = reader.GetInt32(reader.GetOrdinal("MachineID")),
                            MachineName = reader.GetString(reader.GetOrdinal("MachineName")),
                            UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                            UserName = reader.GetString(reader.GetOrdinal("UserName")),
                            ErrorType = reader.GetString(reader.GetOrdinal("ErrorType")),
                            Status = reader.GetString(reader.GetOrdinal("Status")),
                            Description = reader.GetString(reader.GetOrdinal("Description")),
                            DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated"))
                        };

                        errorReports.Add(errorReport);
                    }
                }
            }

            return errorReports;
        }

    }
}
