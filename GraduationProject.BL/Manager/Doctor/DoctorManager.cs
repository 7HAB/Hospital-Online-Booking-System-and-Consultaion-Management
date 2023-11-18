using graduationProject.DAL;
using GraduationProject.BL.Dtos;
using GraduationProject.BL.Dtos.Doctor;
using Microsoft.AspNetCore.Http;
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
                Name = dbDoctor.Name,
                Title = dbDoctor.Title,
                Description = dbDoctor.Description,
                SpecializationName = dbDoctor.specialization.Name,
                WeekSchadual = dbDoctor.weeks
                .Select(d => new WeekScheduleForDoctorsDto
                {
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
        public async Task<List<Doctor>> UploadDoctorImage(string doctorId, List<IFormFile> imageFiles)
        {
            if (imageFiles == null || imageFiles.Count == 0)
            {
                return new List<Doctor>();
            }

            List<Doctor> doctors = new List<Doctor>();

            foreach (var file in imageFiles)
            {
                if (file.Length > 0)
                {
                    var fileExtension = Path.GetExtension(file.FileName);
                    var fakeFileName = $"{Guid.NewGuid().ToString()}{fileExtension}";
                    var storedFileName = "wwwroot/" + "UploadImages/" + fakeFileName;
                    //var storedFileName = Path.Combine("wwwroot", "UploadImages", fakeFileName);



                    var directory = Path.GetDirectoryName(storedFileName);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    Doctor doctor = new Doctor
                    {
                        Id = doctorId,
                        FileName = file.FileName,
                        ContentType = file.ContentType,
                        StoredFileName = storedFileName,
                    };

                    var path = Path.Combine(Directory.GetCurrentDirectory(), storedFileName);

                    using (FileStream fileStream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    doctors.Add(doctor);
                }
            }

            _unitOfWork.doctorRepo.UploadDoctorImage(doctors);
            _unitOfWork.SaveChanges();

            return doctors;
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
}
    