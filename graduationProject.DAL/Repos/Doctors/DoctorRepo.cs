using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graduationProject.DAL
{
    public class DoctorRepo : GenaricRepo<Doctor>, IDoctorRepo
    {
        private readonly HospitalContext _context;

        public DoctorRepo(HospitalContext context) : base(context)
        {
            _context = context;
        }
        public Doctor? GetById(string? id)
        {
            return _context.Set<Doctor>().Include(d => d.specialization).Include(d => d.weeks).FirstOrDefault(d => d.Id == id);
        }
        public List<Doctor> GetAll()
        {
            return _context.Set<Doctor>().Include(d => d.specialization).Include(d => d.weeks).ToList();
        }
        public List<Specialization> GetDoctorsBySpecialization(int SpeializationId)
        { 
            var doctors = _context.Specializations.Include(d => d.Doctors).ThenInclude(d => d.weeks).Where(s => s.Id == SpeializationId).ToList();
            return doctors;
        }
        }
}
