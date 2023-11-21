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
