using graduationProject.DAL;
using graduationProject.DAL.Data.Models;
using GraduationProject.BL.Dtos;
using GraduationProject.BL.Dtos.Doctor;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public List<GetAllSpecializationsDto> GetAllSpecializations()
        {
            List<Specialization> specializations = _unitOfWork.doctorRepo.GetAllSpecializations();
            return specializations.Select(s => new GetAllSpecializationsDto
            {
                Id = s.Id,
                Name = s.Name,
                DoctorsForAllSpecializations = s.Doctors.Select(d => new DoctorsForAllSpecializations
                {
                    Id = d.Id,
                    Name = d.Name
                }).ToList()
            }).ToList();
        }
        public List<GetAllDoctorsDto> GetAllDoctors()
        {
            List<Doctor> doctors = _unitOfWork.doctorRepo.GetAll();

            return doctors.Select(d => new GetAllDoctorsDto
            {

                Id = d.Id,
                Name = d.Name,
                Title = d.Title,
                Description = d.Description,
                SpecializationName = d.specialization.Name,
                ImageFileName = d.FileName,
                ImageStoredFileName = d.StoredFileName,
                ImageContentType = d.ContentType, 
                WeekSchadual = d.weeks
                .Select(d => new WeekScheduleForDoctorsDto
                {
                    Id = d.Id,
                    DayOfWeek = d.DayOfWeek,
                    LimitOfPatients = d.LimitOfPatients,
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
                ID = dbDoctor.Id,
                Name = dbDoctor.Name,
                Title = dbDoctor.Title,
                Description = dbDoctor.Description,
                SpecializationName = dbDoctor.specialization.Name,
                WeekSchadual = dbDoctor.weeks
                .Select(d => new WeekScheduleForDoctorsDto
                {
                    Id = d.Id,
                    DayOfWeek = d.DayOfWeek,
                    StartTime = d.StartTime.ToShortTimeString(),
                    EndTime = d.EndTime.ToShortTimeString(),
                    IsAvailable = d.IsAvailable
                }).ToList(),
                ImageFileName = dbDoctor.FileName,
                ImageStoredFileName = dbDoctor.StoredFileName,
                ImageContentType = dbDoctor.ContentType,
            };
        }

        public List<GetDoctorsBySpecializationDto> GetDoctorsBySpecialization(int id)
        {
            var dbSpecializationDoctors = _unitOfWork.doctorRepo.GetDoctorsBySpecialization(id);
            return dbSpecializationDoctors.Select(s => new GetDoctorsBySpecializationDto
            {
                id = s.Id,
                Name = s.Name,

                ChildDoctorOfSpecializations = s.Doctors
                .Select(d => new ChildDoctorOfSpecializationDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Title = d.Title,
                    Description = d.Description,
                    ImageFileName = d.FileName,
                    ImageStoredFileName = d.StoredFileName,
                    ImageContentType = d.ContentType,
                    WeekSchadual = d.weeks
                .Select(d => new WeekScheduleForDoctorsDto
                {
                    Id= d.Id,
                    DayOfWeek = d.DayOfWeek,
                    LimitOfPatients = d.LimitOfPatients,
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

        public List<GetAllPatientsWithDateDto> GetAllPatientsWithDate(DateTime date, string DoctorId)
        {
            var patients = _unitOfWork.patientRepo.GetAllPatientsByDate(date, DoctorId);
            List<GetAllPatientsWithDateDto> patientsWithDateDtosList = new List<GetAllPatientsWithDateDto>();
            foreach (var patient in patients)
            {
                var patientListItem = new GetAllPatientsWithDateDto
                {
                    PatientId = patient.Id,
                    Name = patient.Name,
                    DateOfBirth = patient.DateOfBirth,
                    Gender = patient.Gender,
                };
                if (patientListItem != null)
                {
                    patientsWithDateDtosList.Add(patientListItem);
                }
            }
            return patientsWithDateDtosList;
        }
        #region Add Visit Count Records
        public void AddVisitCountRecords(DateTime StartDate, DateTime EndDate)
        {
            List<Doctor> doctors = _unitOfWork.doctorRepo.GetAll();
            
            DateTime start = StartDate;
            DateTime end = EndDate;
            int count = end.Day- start.Day;
            DateTime now =DateTime.Now.Date;
            foreach (Doctor doctor in doctors)
            {
                    for (int j = 0; j <= count; j++)
                    {
                  

                    DayOfWeek Day = start.AddDays(j).DayOfWeek;
                         VisitCount v = _unitOfWork.visitCountRepo.GetCount(start.AddDays(j), doctor.Id);
                      if (v == null && StartDate>=now)
                         {
                        WeekSchedule? weekSchedule = _unitOfWork.visitCountRepo.GetWeekSchedule(Day, doctor.Id);


                        if (start.Year == StartDate.Year)
                        {
                            if (weekSchedule != null)
                            {
                                VisitCount visitCount = new VisitCount
                                {
                                    DoctorId = doctor.Id,
                                    Date = start.AddDays(j),
                                    LimitOfPatients = weekSchedule.LimitOfPatients,
                                    WeekScheduleId = weekSchedule.Id,
                                    ActualNoOfPatients = 0,
                                    Day = weekSchedule.DayOfWeek,

                                };


                                _unitOfWork.visitCountRepo.AddVisitCountRecords(visitCount);
                                _unitOfWork.SaveChanges();

                            }
                        }

                    }
                }
            }

        }
        #endregion
        #region get visit count
        public VisitCountDto GetVisitCount(DateTime date, string doctorId)
        {
            VisitCount visitCount = _unitOfWork.visitCountRepo.GetCount(date, doctorId);
            if(visitCount == null) { return null; }
            return new VisitCountDto
            {
                Id = visitCount.Id,
                Date = date.ToShortDateString(),
                DoctorId = doctorId,
                ActualNoOfPatients = visitCount.ActualNoOfPatients,
                LimitOfPatients = visitCount.LimitOfPatients,
                WeekScheduleId = visitCount.WeekScheduleId,
                Day = visitCount.Date.DayOfWeek,

            };
        }
        #endregion
        public GetPatientForDoctorDto? GetPatientForDoctorId(string id)
        {
            Patient? dbPatient = _unitOfWork.patientRepo.GetPatientForDoctor(id);
            if (dbPatient is null)
                return null!;



            return new GetPatientForDoctorDto
            {
                Name = dbPatient.Name,
                Gender = dbPatient.Gender,
                DateOfBirth = dbPatient.DateOfBirth,
                medicaHistory = new GetMedicalHistoryByPhoneDto
                {
                    MartialStatus = dbPatient.MedicaHistory.MartialStatus,
                    Depression = dbPatient.MedicaHistory.Depression,
                    Allergies = dbPatient.MedicaHistory.Allergies,
                    Diabetes = dbPatient.MedicaHistory.Diabetes,
                    Smoker = dbPatient.MedicaHistory.Smoker,
                    AnxityOrPanicDisorder = dbPatient.MedicaHistory.AnxityOrPanicDisorder,
                    Asthma = dbPatient.MedicaHistory.Asthma,
                    HeartDisease = dbPatient.MedicaHistory.HeartDisease,
                    previousSurgeries = dbPatient.MedicaHistory.previousSurgeries,
                    BloodGroup = dbPatient.MedicaHistory.BloodGroup,
                    Hepatitis = dbPatient.MedicaHistory.Hepatitis,
                    HighBloodPressure = dbPatient.MedicaHistory.HighBloodPressure,
                    LowBloodPressure = dbPatient.MedicaHistory.LowBloodPressure,
                    Medication = dbPatient.MedicaHistory.Medication,
                    Other = dbPatient.MedicaHistory.Other,
                    pregnancy = dbPatient.MedicaHistory.pregnancy,
                },
                PatientVisitList = dbPatient.PatientVisits
                .Select(s => new GetPatientVisitsChildDTO
                {
                    Comments = s.Comments,
                    ArrivalTime = s.ArrivalTime,
                    Prescription = s.Prescription,
                    DateOfVisit = s.DateOfVisit,
                    Symptoms = s.Symptoms,
                    VisitStatus = s.VisitStatus,
                    VisitEndTime = s.VisitEndTime,
                    VisitStartTime = s.VisitStartTime

                }).ToList()
            };
        }
        public bool UpdatePatientVisit(UpdatePatientVisitDto updateDto)
        {
            PatientVisit? dbVisit = _unitOfWork.patientVisitRepo.GetById(updateDto.Id);
            if (dbVisit == null) { return false; }
            dbVisit.Comments = updateDto.Comments;
            dbVisit.Symptoms = updateDto.Symptoms;
            dbVisit.Prescription = updateDto.Prescription;
            _unitOfWork.patientVisitRepo.UpdatePatientVisit(dbVisit);
            _unitOfWork.SaveChanges();
            return true;
        }
        #region UploadImage
        public async Task<Doctor> UploadDoctorImage(string doctorId, IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return null;
            }

            var fileExtension = Path.GetExtension(imageFile.FileName);
            var fakeFileName = $"{Guid.NewGuid().ToString()}{fileExtension}";
            var storedFileName = "wwwroot/" + "UploadImages/" + fakeFileName;
            var directory = Path.GetDirectoryName(storedFileName);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), storedFileName);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            Doctor doctor = new Doctor
            {
                Id = doctorId,
                FileName = imageFile.FileName,
                ContentType = imageFile.ContentType,
                StoredFileName = storedFileName,
            };

            _unitOfWork.doctorRepo.UploadDoctorImage(doctor);
            _unitOfWork.SaveChanges();

            return doctor;
        }
    }

        #endregion

        //#region UpdateImge
        //public void UpdateDoctorImage(string doctorId, string fileName, string storedFileName, string contentType)
        //{
        //    if (string.IsNullOrEmpty(doctorId) || string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(contentType))
        //    {
        //        return;
        //    }

        //    // Assuming the original location is in the "UploadImages" folder
        //    var originalFilePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadImages", storedFileName);

        //    // Create a new unique file name for the moved file
        //    var fakeFileName = Path.GetRandomFileName();
        //    var newStoredFileName = Path.Combine("UploadImages", fakeFileName);

        //    var newFilePath = Path.Combine(Directory.GetCurrentDirectory(), newStoredFileName);

        //    var directory = Path.GetDirectoryName(newFilePath);
        //    if (!Directory.Exists(directory))
        //    {
        //        Directory.CreateDirectory(directory);
        //    }

        //    using (FileStream originalFileStream = new FileStream(originalFilePath, FileMode.Open))
        //    {
        //        using (FileStream newFileStream = new FileStream(newFilePath, FileMode.Create))
        //        {
        //            originalFileStream.CopyTo(newFileStream);
        //        }
        //    }

        //    _unitOfWork.doctorRepo.UpdateDoctorImage(doctorId, fileName, newStoredFileName, contentType);
        //    _unitOfWork.SaveChanges();
        //}
        //#endregion
    }

    