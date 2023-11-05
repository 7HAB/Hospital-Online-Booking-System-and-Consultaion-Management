using graduationProject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL
{
    public class PatientManager : IPatientManager
    {
        private readonly PatientRepo _patientRepo;
        public PatientManager(PatientRepo patientRepo)
        {
            _patientRepo = patientRepo;
        }
        public List<GetAllDoctorsDto>? GetAllDoctors()
        {
            List<Doctor> doctors = _patientRepo.GetAllDoctors();

            return doctors.Select(d => new GetAllDoctorsDto
            {
                Name = d.Name,
                Title = d.Title,
                Description = d.Description,
                Specialization = d.Specialization,
                PerformanceRate = d.PerformanceRate,
                weeks = d.weeks
            }).ToList();


        }
    }
}
