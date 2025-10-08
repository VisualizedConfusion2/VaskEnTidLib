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
    public class MachineRepo
    {
        private readonly string _connectionString;

        public MachineRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Machine> GetMachinesByUserId(int userId)
        {
            var machines = new List<Machine>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("usp_SelectMachinesByUserID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var machine = new Machine
                        {
                            MachineID = reader.GetInt32(reader.GetOrdinal("MachineID")),
                            MachineType = reader.GetString(reader.GetOrdinal("MachineType")),
                            DepartmentID = reader.GetInt32(reader.GetOrdinal("DepartmentID")),
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        };

                        machines.Add(machine);
                    }
                }
            }

            return machines;
        }


    }
}
