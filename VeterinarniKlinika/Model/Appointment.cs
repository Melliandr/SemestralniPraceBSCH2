using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinarniKlinika.Model
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public int VeterinarianID { get; set; }
        public int AnimalID { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string ServiceType { get; set; }
        public string Comments { get; set; }
        public int Status { get; set; }
    }

}
