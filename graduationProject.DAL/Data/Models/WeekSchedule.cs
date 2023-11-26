using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graduationProject.DAL.Data.Models
{
    public class WeekSchedule
    {
        public int Id { get; set; }

        [Required]
        public DayOfWeek? DayOfWeek { get; set; }

        [Required]
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsAvailable { get; set; }
        public int LimitOfPatients { get; set; }

        [Required]
        //public DateTime ScheduleDate { get; set; }
        public Doctor? Doctor { get; set; }

        public string? DoctorId { get; set; }
        public ICollection<VisitCount>? VisitCount { get; set; } = new HashSet<VisitCount>();
    }
}


