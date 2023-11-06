using graduationProject.DAL;
using graduationProject.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Dtos.Doctor
{
    public class GetAllDoctorsDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string Specialization { get; set; } = "";
        public int PerformanceRate { get; set; }

        public ICollection<WeekSchedule> weeks { get; set; } = new HashSet<WeekSchedule>();

    }
}
