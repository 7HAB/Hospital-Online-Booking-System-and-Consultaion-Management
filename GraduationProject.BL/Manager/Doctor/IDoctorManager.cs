using graduationProject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraduationProject.BL.Dtos.Doctor;

namespace GraduationProject.BL
{
    public interface IDoctorManager
    {
        public List<GetAllDoctorsDto> GetAllDoctors();
        public List<GetDoctorsBySpecializationDto> GetDoctorsBySpecialization(int id);
    }
}
