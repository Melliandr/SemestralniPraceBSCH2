using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinarniKlinika.Model;

namespace VeterinarniKlinika.ViewModel.Repository
{
    public class AnimalRepository
    {
        private readonly string _connectionString;

        public AnimalRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CreateAnimal(Animal animal)
        {
            try
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO Animal (Name, Species, Breed, DateOfBirth) VALUES (@Name, @Species, @Breed, @DateOfBirth)";
                        command.Parameters.AddWithValue("@Name", animal.Name);
                        command.Parameters.AddWithValue("@Species", animal.Species);
                        command.Parameters.AddWithValue("@Breed", animal.Breed);
                        command.Parameters.AddWithValue("@DateOfBirth", animal.DateOfBirth?.ToString("yyyy-MM-dd"));

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateAnimal: {ex.Message}");
                
            }
        }




        public List<Animal> GetAllAnimals()
        {
            var animals = new List<Animal>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Animal";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            animals.Add(new Animal
                            {
                                AnimalID = Convert.ToInt32(reader["AnimalID"]),
                                Name = reader["Name"].ToString(),
                                Species = reader["Species"].ToString(),
                                Breed = reader["Breed"].ToString(),
                                DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                            });
                        }
                    }
                }
            }
            return animals;
        }

        public void UpdateAnimal(Animal animal)
        {
            try
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE Animal SET Name = @Name, Species = @Species, Breed = @Breed, DateOfBirth = @DateOfBirth WHERE AnimalID = @AnimalID";
                        command.Parameters.AddWithValue("@AnimalID", animal.AnimalID);
                        command.Parameters.AddWithValue("@Name", animal.Name);
                        command.Parameters.AddWithValue("@Species", animal.Species);
                        command.Parameters.AddWithValue("@Breed", animal.Breed);
                        command.Parameters.AddWithValue("@DateOfBirth", animal.DateOfBirth?.ToString("yyyy-MM-dd"));

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateAnimal: {ex.Message}");
                
            }
        }


        public void DeleteAnimal(int animalId)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM Animal WHERE AnimalID = @AnimalID";
                    command.Parameters.AddWithValue("@AnimalID", animalId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
