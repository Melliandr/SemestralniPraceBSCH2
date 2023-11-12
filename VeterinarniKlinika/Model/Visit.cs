using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinarniKlinika.Model
{
    public class Visit
    {
        public int VisitID { get; set; }
        public int AnimalID { get; set; }
        public DateTime VisitDate { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
    }
}
