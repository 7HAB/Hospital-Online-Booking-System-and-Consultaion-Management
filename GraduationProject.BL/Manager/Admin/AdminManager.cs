using graduationProject.DAL;
using graduationProject.DAL.Data.Models;
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
                if(doctor.Status == true)
                {
                    doctor.Status = false;
                }
                else
                {
                    doctor.Status = true;
                }
                _unitOfWork.SaveChanges();
            }
            return doctor;   
        }
        #endregion
        #region Update Doctor by Id

        public Doctor UpdateDoctorById(UpdateDoctorStatusDto updateDoctor, string id)
        {
            Doctor? doctor = _unitOfWork.doctorRepo.GetById(id);

            if (doctor != null)
            {
                doctor.UserName = updateDoctor.PhoneNumber;
                doctor.PhoneNumber = updateDoctor.PhoneNumber;
                doctor.Name = updateDoctor.Name;
                doctor.Title = updateDoctor.Title;
                doctor.Salary = updateDoctor.Salary;
                doctor.Description = updateDoctor.Description;
                doctor.DateOfBirth = updateDoctor.DateOfBirth;
                //doctor.AssistantID = updateDoctor.AssistantID;
                //doctor.AssistantDateOfBirth = updateDoctor.AssistantDateOfBirth;
                //doctor.AssistantPhoneNumber = updateDoctor.AssistantPhoneNumber;
                //doctor.AssistantName = updateDoctor.AssistantName;
                doctor.Status = updateDoctor.Status;

                _unitOfWork.adminRepo.UpdateDoctorById(doctor.Id);
                _unitOfWork.SaveChanges();
            }

            return doctor;
        }
        #endregion
        #region Get Doctor By ID For Admin
        public GetDoctorByIDForAdminDto GetDoctorByIdForAdmin(string id)
        {
            Doctor? doctor = _unitOfWork.doctorRepo.GetById(id);
            if (doctor is null)
                return null!;

            return new GetDoctorByIDForAdminDto
            {
                ID = doctor.Id,
                DateOfBirth = doctor.DateOfBirth,
                Name = doctor.Name,
                PhoneNumber = doctor.PhoneNumber,
                Title = doctor.Title,
                Salary = doctor.Salary,
                Description = doctor.Description,
                SpecializationName = doctor.specialization.Name,
                WeekSchadual = doctor.weeks
                .Select(d => new WeekScheduleForDoctorsDto
                {
                    Id = d.Id,
                    DayOfWeek = d.DayOfWeek,
                    StartTime = d.StartTime?.ToShortTimeString(),
                    EndTime = d.EndTime?.ToShortTimeString(),
                    IsAvailable = d.IsAvailable,
                    LimitOfPatients = d.LimitOfPatients,
                }).ToList(),
                ImageFileName = doctor.FileName,
                ImageStoredFileName = doctor.StoredFileName,
                ImageContentType = doctor.ContentType,
            };
        }
        #endregion
        #region Add Week Schedule
        public void AddWeekSchedule(AddWeekScheduleDto addWeekSchedule)
        {
            WeekSchedule weekSchedule = new WeekSchedule
            {
                DayOfWeek = addWeekSchedule.DayOfWeek,
                LimitOfPatients = addWeekSchedule.LimitOfPatients,
                StartTime = addWeekSchedule.StartTime,
                EndTime = addWeekSchedule.EndTime,
                DoctorId = addWeekSchedule.DoctorId,
                IsAvailable = addWeekSchedule.IsAvailable,
            };
            _unitOfWork.adminRepo.AddWeekSchedule(weekSchedule);
        }
        #endregion
    }
}
