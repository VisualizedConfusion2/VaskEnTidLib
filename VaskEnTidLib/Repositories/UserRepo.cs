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

//Updates a users information by their UserID using the stored procedure usp_UpdateUserByID any null values will not overwrite existing data handled by COALESCE in SQl

        public bool UpdateUserById(
            int userId,
            string? apartmentNumber = null,
            string? name = null,
            string? phone = null,
            string? email = null,
            string? password = null)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("usp_UpdateUserByID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@ApartmentNumber", (object?)apartmentNumber ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Name", (object?)name ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Phone", (object?)phone ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", (object?)email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Password", (object?)password ?? DBNull.Value);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        Debug.WriteLine($"No user found with UserID {userId}.");
                        return false;
                    }

                    Debug.WriteLine($"User {userId} updated successfully.");
                    return true;
                }
                catch (SqlException ex)
                {
                    Debug.WriteLine($"SQL error during user update: {ex.Message}");
                    return false;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Unexpected error during user update: {ex.Message}");
                    return false;
                }
            }
        }
    }
}
