using graduationProject.DAL;
using graduationProject.DAL.Data.Models;
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
                doctor.AssistantID = updateDoctor.AssistantID;
                doctor.AssistantDateOfBirth = updateDoctor.AssistantDateOfBirth;
                doctor.AssistantPhoneNumber = updateDoctor.AssistantPhoneNumber;
                doctor.AssistantName = updateDoctor.AssistantName;
                doctor.Status = updateDoctor.Status;

                _unitOfWork.adminRepo.UpdateDoctorById(doctor);
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
                    StartTime = d.StartTime.ToShortTimeString(),
                    EndTime = d.EndTime.ToShortTimeString(),
                    IsAvailable = d.IsAvailable
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
