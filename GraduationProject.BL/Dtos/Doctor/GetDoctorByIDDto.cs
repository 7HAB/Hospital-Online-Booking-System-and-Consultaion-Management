using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL
{
    public class GetDoctorByIDDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string SpecializationName { get; set; } = "";
        public List<WeekScheduleForDoctorsDto>? WeekSchadual { get; set; }
    }
}
