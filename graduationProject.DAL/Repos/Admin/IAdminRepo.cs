using graduationProject.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graduationProject.DAL
{
    public interface IAdminRepo
    {
        public void UpdateDoctorById(Doctor doctor);
        public void AddWeekSchedule(WeekSchedule schedule);

    }
}
