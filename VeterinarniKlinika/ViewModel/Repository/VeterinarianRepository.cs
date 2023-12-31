using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinarniKlinika.Model;

namespace VeterinarniKlinika.ViewModel.Repository
{
    public class VeterinarianRepository
    {
        private readonly string _connectionString;

        public VeterinarianRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CreateVeterinarian(Veterinarian veterinarian)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(veterinarian.FirstName) ||
                    string.IsNullOrWhiteSpace(veterinarian.LastName))
                {
                    
                    Console.WriteLine("First Name and Last Name are required fields.");
                    return; 
                }

                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO Veterinarian (FirstName, LastName, Specialization, PhoneNumber, BirthDate, Address) VALUES (@FirstName, @LastName, @Specialization, @PhoneNumber, @BirthDate, @Address)";
                        command.Parameters.AddWithValue("@FirstName", veterinarian.FirstName);
                        command.Parameters.AddWithValue("@LastName", veterinarian.LastName);
                        command.Parameters.AddWithValue("@Specialization", veterinarian.Specialization ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PhoneNumber", veterinarian.PhoneNumber ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@BirthDate", veterinarian.BirthDate?.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@Address", veterinarian.Address ?? (object)DBNull.Value);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateVeterinarian: {ex.Message}");
                throw;
            }
        }


        public List<Veterinarian> GetAllVeterinarians()
        {
            var veterinarians = new List<Veterinarian>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Veterinarian";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            veterinarians.Add(new Veterinarian
                            {
                                VeterinarianID = Convert.ToInt32(reader["VeterinarianID"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Specialization = reader["Specialization"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                BirthDate = Convert.ToDateTime(reader["BirthDate"]),
                                Address = reader["Address"].ToString()
                            });
                        }
                    }
                }
            }
            return veterinarians;
        }

        public void UpdateVeterinarian(Veterinarian veterinarian)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE Veterinarian SET FirstName = @FirstName, LastName = @LastName, Specialization = @Specialization, PhoneNumber = @PhoneNumber, BirthDate = @BirthDate, Address = @Address WHERE VeterinarianID = @VeterinarianID";
                    command.Parameters.AddWithValue("@VeterinarianID", veterinarian.VeterinarianID);
                    command.Parameters.AddWithValue("@FirstName", veterinarian.FirstName);
                    command.Parameters.AddWithValue("@LastName", veterinarian.LastName);
                    command.Parameters.AddWithValue("@Specialization", veterinarian.Specialization);
                    command.Parameters.AddWithValue("@PhoneNumber", veterinarian.PhoneNumber);
                    command.Parameters.AddWithValue("@BirthDate", veterinarian.BirthDate?.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@Address", veterinarian.Address);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteVeterinarian(int veterinarianId)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM Veterinarian WHERE VeterinarianID = @VeterinarianID";
                    command.Parameters.AddWithValue("@VeterinarianID", veterinarianId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
