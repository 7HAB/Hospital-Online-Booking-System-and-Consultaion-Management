using graduationProject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL
{
    public interface IAdminManager
    {
        public Doctor UpdateDoctorById(UpdateDoctorStatusDto updateDoctor, string id);
        public void AddWeekSchedule(AddWeekScheduleDto addWeekSchedule);

    }
}
