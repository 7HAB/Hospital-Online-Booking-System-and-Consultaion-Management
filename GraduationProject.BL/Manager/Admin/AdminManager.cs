using graduationProject.DAL;
using GraduationProject.BL.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL
{
    public class AdminManager : IAdminManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public AdminManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #region get all specializations and doctors for admins
        public List<GetAllSpecializationForAdminDto> GetAllSpecializations()
        {
            List<Specialization> specializations = _unitOfWork.doctorRepo.GetAllSpecializations();
            return specializations.Select(s => new GetAllSpecializationForAdminDto
            {
                Id = s.Id,
                Name = s.Name,
                DoctorsForAdmin = s.Doctors.Select(d => new GetAllDoctorsForAdminDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Title = d.Title,
                    Description = d.Description,
                    Salary = d.Salary,
                    DateOfBirth = d.DateOfBirth.ToShortDateString(),
                    Status = d.Status,
                    AssistantID = d.AssistantID,
                    AssistantName = d.AssistantName,
                    AssistantPhoneNumber= d.AssistantPhoneNumber,
                    AssistantDateOfBirth = d.DateOfBirth.ToShortDateString(),
                }).ToList()
            }).ToList();
        }
        #endregion
        #region Get Admin By Phone Number
        public GetAdminByPhoneNumberDto? GetAdminByPhoneNumber(string phoneNumber)
        {
            Admin? dbAdmin = _unitOfWork.adminRepo.GetAdminByPhoneNumber(phoneNumber);
            if (dbAdmin is null)
            { return null; }
            int? Sid = dbAdmin.SpecializationId;
            Specialization specialization = _unitOfWork.adminRepo.GetSpecializationByAdmin(Sid);
            return new GetAdminByPhoneNumberDto
            {
                PhoneNumber = dbAdmin.PhoneNumber,
                Id = dbAdmin.Id,
                Name = dbAdmin.Name,
                SpecializationName = specialization.Name,
            };

        }
        #endregion
        #region Update Doctor by Id

        public Doctor UpdateDoctorById(UpdateDoctorStatusDto updateDoctor , string Id)
        {
            Doctor? doctor = _unitOfWork.adminRepo.UpdateDoctorById(Id);

            if (doctor != null)
            {
                
                doctor.Salary = updateDoctor.Salary;
                doctor.AssistantID = updateDoctor.AssistantID;
                doctor.AssistantDateOfBirth = updateDoctor.AssistantDateOfBirth;
                doctor.AssistantPhoneNumber = updateDoctor.AssistantPhoneNumber;
                doctor.AssistantName = updateDoctor.AssistantName;
                doctor.Status = updateDoctor.Status;


                _unitOfWork.SaveChanges();
            }

            return doctor;
        }
        #endregion


    }
}
