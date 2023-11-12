using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinarniKlinika.Model
{
    public class Animal
    {
        public int AnimalID { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int OwnerID { get; set; }
    }
}
