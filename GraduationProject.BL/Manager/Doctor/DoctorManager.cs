using graduationProject.DAL;
using GraduationProject.BL.Dtos.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
                SpecializationName = d.specialization.Name,
                WeekSchadual = d.weeks
                .Select(d => new WeekSshaduakForDoctorDto
                {
                    DayOfWeek = d.DayOfWeek,
                    StartTime = d.StartTime.ToShortTimeString(),
                    EndTime = d.EndTime.ToShortTimeString(),
                    IsAvailable = d.IsAvailable
                }).ToList()
            }).ToList();
        }



        public GetDoctorByIDDto GetDoctorBYId(string id)
        {
            Doctor? dbDoctor = _unitOfWork.doctorRepo.GetById(id);
            if (dbDoctor is null)
                return null!;
            return new GetDoctorByIDDto
            {
                Name = dbDoctor.Name,
                Title = dbDoctor.Title,
                Description = dbDoctor.Description,
                SpecializationName = dbDoctor.specialization.Name,
                WeekSchadual = dbDoctor.weeks
                .Select(d => new WeekSshaduakForDoctorDto
                {
                    DayOfWeek = d.DayOfWeek,
                    StartTime = d.StartTime.ToShortTimeString(),
                    EndTime = d.EndTime.ToShortTimeString(),
                    IsAvailable = d.IsAvailable
                }).ToList()
            };
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
                    Description = d.Description,
                    WeekSchadual = d.weeks
                .Select(d => new WeekSshaduakForDoctorDto
                {
                    DayOfWeek = d.DayOfWeek,
                    StartTime = d.StartTime.ToShortTimeString(),
                    EndTime = d.EndTime.ToShortTimeString(),
                    IsAvailable = d.IsAvailable
                }).ToList()
                }).ToList()
            }).ToList();
        }

        public GetAllWeekScheduleDto? GetAllWeekScheduleByDoctorId(string id)
        {
            var doctor = _unitOfWork.weekScheduleRepo.GetAllWeekSchedule(id);
            return new GetAllWeekScheduleDto
            {

                Name = doctor.Name,
                WeekSchedule = doctor.weeks.Select(d => new GetAllWeekScheduleChildDto
                {
                    DayOfWeek = d.DayOfWeek,
                    IsAvailable = d.IsAvailable,
                    StartTime = d.StartTime.ToShortTimeString(),
                    EndTime = d.EndTime.ToShortTimeString(),
                }).ToList()
            };
        }

        public List<GetAllPatientsWithDateDto> GetAllPatientsWithDate(DateTime date , string DoctorId)
        {
            var patients = _unitOfWork.patientRepo.GetAllPatientsByDate(date, DoctorId);
            List <GetAllPatientsWithDateDto> patientsWithDateDtosList = new List<GetAllPatientsWithDateDto>();
            foreach(var patient in patients)
            {
                var patientListItem = new GetAllPatientsWithDateDto
                {
                    Name = patient.Name,
                    DateOfBirth = patient.DateOfBirth,
                    Gender = patient.Gender,
                    MedicaHistory = patient.MedicaHistory
                };
                if(patientListItem != null)
                {
                    patientsWithDateDtosList.Add(patientListItem);
                }
            }
            return patientsWithDateDtosList;
        }
    }
}
