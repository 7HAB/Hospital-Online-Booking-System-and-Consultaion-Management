using graduationProject.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graduationProject.DAL
{
    public class VisitCount
    {
        public int Id { get; set; }
        public DateTime Date {  get; set; }
        public int LimitOfPatients { get; set; }
        public int ActualNoOfPatients { get; set; }
        public WeekSchedule? WeekSchedule { get; set; }
    }
}
