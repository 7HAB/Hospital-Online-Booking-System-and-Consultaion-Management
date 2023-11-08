using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL
{
    public class GetAllWeekScheduleChildDto
    {
        public string? DayOfWeek { get; set; }
        public bool IsAvailable { get; set; }


        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; } 

    }
}
