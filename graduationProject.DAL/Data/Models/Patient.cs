using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using ServiceStack.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace graduationProject.DAL
{
    public class Patient : IdentityUser
    {
        public string? Id { get; set; } //by auto genrating
        /*public string? Name { get; set; }*/
        public string? Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Unique]
        [RegularExpression(@"^0\d{10}$", ErrorMessage = "Please enter a valid 11-digit mobile number starting with '0'.")]
/*        public int PhoneNumber { get; set; } //by auto genrating
*/        public MedicaHistory? MedicaHistory { get; set; }
        public ICollection<Reception>? Receptions { set; get; } = new HashSet<Reception>();
        public ICollection<PatientVisit> PatientVisits { set; get; } = new HashSet<PatientVisit>();
        public ICollection<PatientVisitsWithDoctor> patientVisitsWithDoctors { set; get; }= new HashSet<PatientVisitsWithDoctor>();

    }
}
