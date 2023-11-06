using graduationProject.DAL;
using GraduationProject.BL.Dtos.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL
{
    public class DoctorManager : IDoctorManager
    {
        // private readonly PatientRepo _patientRepo;
        private readonly IUnitOfWork _unitOfWork;
        public DoctorManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<GetAllDoctorsDto> GetAllDoctors()
        {
            List<Doctor> doctors = _unitOfWork.doctorRepo.GetAll();

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
        public List<GetDoctorsBySpecializationDto> GetDoctorsBySpecialization(int id)
        {
            var dbSpecializationDoctors = _unitOfWork.doctorRepo.GetDoctorsBySpecialization(id);
            return dbSpecializationDoctors.Select(s => new GetDoctorsBySpecializationDto
            {
                Name = s.Name,
                ChildDoctorOfSpecializations = s.Doctors
                .Select(d => new ChildDoctorOfSpecializationDto 
                {
                    Name = d.Name,
                    Title = d.Title,
                    Description = d.Description
                }).ToList()
            }).ToList();
        }

    }
    }
