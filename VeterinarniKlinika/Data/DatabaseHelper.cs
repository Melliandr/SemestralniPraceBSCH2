using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinarniKlinika.Model;

namespace VeterinarniKlinika
{
    public class DatabaseHelper
    {
        private static readonly string connectionString = "Data Source=C:\\Users\\Konstantin\\Desktop\\SemestralniPraceC#\\SemestralniPraceBSCH2\\VeterinarniKlinika\\Data\\veterinarniKlinika.db";

        public static string GetConnectionString()
        {
            return connectionString;
        }
    }
}