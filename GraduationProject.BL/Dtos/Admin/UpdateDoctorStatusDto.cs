using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL
{
    public class UpdateDoctorStatusDto
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public decimal Salary { get; set; }

        public string? AssistantID { get; set; }

        [Required]
        public string? AssistantName { get; set; }

        [Unique]
        public string? AssistantPhoneNumber { get; set; }

        public DateTime AssistantDateOfBirth { get; set; }

        public string? Status { get; set; } 

    }
}
