using graduationProject.DAL;
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
        #region Adding Specialization
        public void AddSpecialization(AddSpecializationDto? specialization)
        {
            Specialization dbSpecialization = new Specialization
            {
                Name = specialization.Name,
            };
            _unitOfWork.adminRepo.AddSpecialization(dbSpecialization);
            _unitOfWork.SaveChanges();
        }
        #endregion
        #region ChangeStatus
        public Doctor ChangeDoctorStatus(string doctorId)
        {
            Doctor? doctor = _unitOfWork.adminRepo.ChangeDoctorStatus(doctorId);
            if (doctor != null)
            {
                if(doctor.Status == "Active")
                {
                    doctor.Status = "Not Active";
                }
                else
                {
                    doctor.Status = "Active";
                }
                _unitOfWork.SaveChanges();
            }
            return doctor;   
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
