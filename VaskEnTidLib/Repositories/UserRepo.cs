using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using VaskEnTidLib.Models;

namespace VaskEnTidLib.Repositories
{
    public class UserRepo
    {
        private readonly string _connectionString;
        public UserRepo(string connectionString) => _connectionString = connectionString;

        public User? GetUserByEmail(string email)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var cmd = new SqlCommand(@"
            SELECT u.UserId, u.ApartmentNumber, u.Name, u.Phone, u.Email, u.Password,
                   udm.DepartmentID, udm.UserTypeID
            FROM Users u
            INNER JOIN UserDepartmentMappings udm ON u.UserId = udm.UserID
            WHERE u.Email = @Email
        ", conn);

            cmd.Parameters.AddWithValue("@Email", email);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    UserId = (int)reader["UserId"],
                    ApartmentNumber = reader["ApartmentNumber"].ToString() ?? string.Empty,
                    Name = reader["Name"].ToString() ?? string.Empty,
                    Phone = reader["Phone"].ToString() ?? string.Empty,
                    Email = reader["Email"].ToString() ?? string.Empty,
                    Password = reader["Password"].ToString() ?? string.Empty,
                    DepartmentID = (int)reader["DepartmentID"],
                    UserTypeID = (int)reader["UserTypeID"]
                };
            }

            return null;
        }

        public User? RegisterUserByCreationCode(string creationCode, string phone, string email, string password)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("usp_InsertUserFromTempUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CreationCode", creationCode);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return GetUserByEmail(email);
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"Fejl ved oprettelse af bruger: {ex.Message}");
                    return null;
                }
            }
        }

    }
}
