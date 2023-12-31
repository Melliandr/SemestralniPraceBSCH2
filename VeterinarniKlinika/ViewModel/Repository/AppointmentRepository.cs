using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinarniKlinika.Model;

namespace VeterinarniKlinika.ViewModel.Repository
{
    public class AppointmentRepository
    {
        private readonly string _connectionString;

        public AppointmentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CreateAppointment(Appointment appointment)
        {
            try
            {
                if (appointment.VeterinarianID == null || appointment.AnimalID == null || appointment.AppointmentDate == null || string.IsNullOrWhiteSpace(appointment.ServiceType))
                {
                    Console.WriteLine("Not all data for creating a record has been completed. Please enter all required data.");
                    return;
                }

                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO Appointment (VeterinarianID, AnimalID, AppointmentDate, ServiceType, Comments, Status) VALUES (@VeterinarianID, @AnimalID, @AppointmentDate, @ServiceType, @Comments, @Status)";
                        command.Parameters.AddWithValue("@VeterinarianID", appointment.VeterinarianID);
                        command.Parameters.AddWithValue("@AnimalID", appointment.AnimalID);
                        command.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate?.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@ServiceType", appointment.ServiceType);
                        command.Parameters.AddWithValue("@Comments", appointment.Comments ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Status", appointment.Status);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateAppointment: {ex.Message}");
                throw;
            }
        }


        public List<Appointment> GetAllAppointments()
        {
            var appointments = new List<Appointment>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Appointment";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            appointments.Add(new Appointment
                            {
                                AppointmentID = Convert.ToInt32(reader["AppointmentID"]),
                                VeterinarianID = Convert.ToInt32(reader["VeterinarianID"]),
                                AnimalID = Convert.ToInt32(reader["AnimalID"]),
                                AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                                ServiceType = reader["ServiceType"].ToString(),
                                Comments = reader["Comments"].ToString(),
                                Status = Convert.ToInt32(reader["Status"])
                            });
                        }
                    }
                }
            }
            return appointments;
        }

        public void UpdateAppointment(Appointment appointment)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE Appointment SET VeterinarianID = @VeterinarianID, AnimalID = @AnimalID, AppointmentDate = @AppointmentDate, ServiceType = @ServiceType, Comments = @Comments, Status = @Status WHERE AppointmentID = @AppointmentID";
                    command.Parameters.AddWithValue("@AppointmentID", appointment.AppointmentID);
                    command.Parameters.AddWithValue("@VeterinarianID", appointment.VeterinarianID);
                    command.Parameters.AddWithValue("@AnimalID", appointment.AnimalID);
                    command.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate?.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@ServiceType", appointment.ServiceType);
                    command.Parameters.AddWithValue("@Comments", appointment.Comments);
                    command.Parameters.AddWithValue("@Status", appointment.Status);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteAppointment(int appointmentId)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM Appointment WHERE AppointmentID = @AppointmentID";
                    command.Parameters.AddWithValue("@AppointmentID", appointmentId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
