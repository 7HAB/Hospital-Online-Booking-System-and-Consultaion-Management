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
        public void UpdateDoctorById(Doctor doctor)
        {
              _context.Set<Doctor>().Update(doctor);
                
            
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
