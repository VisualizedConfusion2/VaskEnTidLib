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

        public List<ErrorReport> GetAllErrorReports()
        {
            var errorReports = new List<ErrorReport>();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("usp_SelectFromErrorReports", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var report = new ErrorReport
                        {
                            ErrorID = reader.GetInt32(reader.GetOrdinal("ErrorID")),
                            MachineID = reader.GetInt32(reader.GetOrdinal("MachineID")),
                            UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                            ErrorTypeID = reader.GetInt32(reader.GetOrdinal("ErrorTypeID")),
                            StatusID = reader.GetInt32(reader.GetOrdinal("StatusID")),
                            Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                            DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated"))
                        };

                        errorReports.Add(report);
                    }
                }
            }

            return errorReports;
        }
    }
}
