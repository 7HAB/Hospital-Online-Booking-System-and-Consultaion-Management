using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graduationProject.DAL
{
    public class PatientVisitsWithDoctor
    {
        public int Id { get; set; }
        public Patient? Patient { get; set; }
        public PatientVisit? PatientVisit { get; set; }
        public Doctor? Doctor { get; set; }
    }
}
