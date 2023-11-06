using graduationProject.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL
{
    public class PatientManager : IPatientManager
    {
        // private readonly PatientRepo _patientRepo;
        private readonly IUnitOfWork _unitOfWork;
        public PatientManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }




        //public List<GetAllDoctorsDto> GetAllDoctors()
        //{
        //    List<Doctor> doctors = _unitOfWork.doctorRepo.GetAllDoctors();

        //    return doctors.Select(d => new GetAllDoctorsDto
        //    {
        //        Name = d.Name,
        //        Title = d.Title,
        //        Description = d.Description,
        //        Specialization = d.Specialization,
        //        PerformanceRate = d.PerformanceRate,
        //        weeks = d.weeks
        //    }).ToList();


        //}
        #region GetPatientByPhone
        public GetPatientByPhoneDTO? getPatientByPhoneDTO(string phoneNumber)
        {
           Patient? patient = _unitOfWork.patientRepo.GetPatientByPhoneNumber(phoneNumber);

            if (patient == null) { return null; }
            return  new GetPatientByPhoneDTO
            {
                Name = patient.Name,
                PhoneNumber = phoneNumber, 
                Gender = patient.Gender,
                DateOfBirth = patient.DateOfBirth,
            };
        }
        #endregion

    }
}  
    
