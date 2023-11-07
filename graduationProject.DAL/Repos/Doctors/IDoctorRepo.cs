using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graduationProject.DAL
{
    public interface IDoctorRepo : IGenaricRepo<Doctor>
    {
        public Doctor? GetById(string? id);
        public List<Specialization> GetDoctorsBySpecialization(int SpeializationId);
    }
}
