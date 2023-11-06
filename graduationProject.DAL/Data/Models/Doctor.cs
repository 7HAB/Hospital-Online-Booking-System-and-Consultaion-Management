using graduationProject.DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graduationProject.DAL
{
    public class Doctor 
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string Specialization { get; set; } = "";
        public decimal Salary { get; set; }
        public int PerformanceRate { get; set; }

        public string? Review { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Unique]
       /* public int PhoneNumber { get; set; }*/

        public int AssistantID { get; set; }

        [Required]
        public string AssistantName { get; set; } = string.Empty;

        [Unique]
        public int AssistantPhoneNumber { get; set; }

        public DateTime AssistantDateOfBirth { get; set; }
        public ICollection<WeekSchedule> weeks { get; set; } = new HashSet<WeekSchedule>();
        public ICollection<PatientVisitsWithDoctor> patientVisits { get; set; } = new HashSet<PatientVisitsWithDoctor>();
        public Specialization? specialization {  get; set; }
    }
}
