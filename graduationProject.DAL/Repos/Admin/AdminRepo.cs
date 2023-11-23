using Microsoft.EntityFrameworkCore;
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

        public Admin? GetAdminByPhoneNumber(string PhoneNumber)
        {
            return _context.Set<Admin>().FirstOrDefault(A => A.PhoneNumber == PhoneNumber);
            
        }
        public Specialization GetSpecializationByAdmin(int? id)
        {
            return _context.Set<Specialization>().Find(id)!;
        }

        public Doctor? UpdateDoctorById(string doctorId)
        {
            Doctor? doctorToUpdate = _context.Set<Doctor>().Include(A => A.specialization).FirstOrDefault(d => d.Id == doctorId);

            if (doctorToUpdate != null)
            {
                 _context.Set<Doctor>().Update(doctorToUpdate);
                
            }
            return doctorToUpdate;
        }
    }
}
