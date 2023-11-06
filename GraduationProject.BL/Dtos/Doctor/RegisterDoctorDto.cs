using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Dtos
{
    public class RegisterDoctorDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Title { get; set; }
        public string? Description { get; set; }
        //public string Specialization { get; set; } = "";
        public decimal Salary { get; set; }
        public string? PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int AssistantID { get; set; }

        [Required]
        public string AssistantName { get; set; } = string.Empty;

        [Unique]
        public int AssistantPhoneNumber { get; set; }

        public DateTime AssistantDateOfBirth { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
