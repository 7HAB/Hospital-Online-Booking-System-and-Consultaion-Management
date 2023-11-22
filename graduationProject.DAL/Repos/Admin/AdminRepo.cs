using graduationProject.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graduationProject.DAL
{
    public class AdminRepo : IAdminRepo
    {
        private readonly HospitalContext _context;
        public AdminRepo(HospitalContext context)
        {
              _context = context;
        }
        public Doctor? UpdateDoctorById(string doctorId)
        {
            Doctor? doctorToUpdate = _context.Set<Doctor>().FirstOrDefault(d => d.Id == doctorId);

            if (doctorToUpdate != null)
            {
                 _context.Set<Doctor>().Update(doctorToUpdate);
                
            }
            return doctorToUpdate;
        }
        
        #region add week schedule
        public void AddWeekSchedule(WeekSchedule schedule)
        {

            _context.Set<WeekSchedule>().Add(schedule);
            _context.SaveChanges();
        }
        #endregion
    }

}
