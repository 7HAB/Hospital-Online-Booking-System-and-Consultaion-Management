using graduationProject.DAL;
using GraduationProject.BL.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
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

        #region GetPatientByPhone
        public GetPatientByPhoneDTO? getPatientByPhoneDTO(string phoneNumber)
        {
            Patient? patient = _unitOfWork.patientRepo.GetPatientByPhoneNumber(phoneNumber);

            if (patient == null) { return null; }
            return new GetPatientByPhoneDTO
            {
                Name = patient.Name,
                PhoneNumber = phoneNumber,
                Gender = patient.Gender,
                DateOfBirth = patient.DateOfBirth,
            };
        }
        #endregion

        #region GetMedicalHistory
        public GetMedicalHistoryByPhoneDto? GetMedicalHistoryByPhoneNumber(string phoneNumber)
        {
            MedicaHistory? medicalHistory = _unitOfWork.patientRepo.GetMedicaHistoryByPhoneNumber(phoneNumber);
            if (medicalHistory == null) { return null; }

            return new GetMedicalHistoryByPhoneDto
            {
                MartialStatus = medicalHistory.MartialStatus,
                pregnancy = medicalHistory.pregnancy,
                BloodGroup = medicalHistory.BloodGroup,
                previousSurgeries = medicalHistory.previousSurgeries,
                Medication = medicalHistory.Medication,
                Smoker = medicalHistory.Smoker,
                Diabetes = medicalHistory.Diabetes,
                HighBloodPressure = medicalHistory.HighBloodPressure,
                LowBloodPressure = medicalHistory.LowBloodPressure,
                Asthma = medicalHistory.Asthma,
                Hepatitis = medicalHistory.Hepatitis,
                HeartDisease = medicalHistory.HeartDisease,
                AnxityOrPanicDisorder = medicalHistory.AnxityOrPanicDisorder,
                Depression = medicalHistory.Depression,
                Allergies = medicalHistory.Allergies,
                Other = medicalHistory.Other
            };

        }
        #endregion


        #region GetPatientVisitsByPhone
        public GetPatientVisitDto? GetPatientVisitsByPhoneNumber(string phoneNumber)
        {
            /*                List<PatientVisit>? patientVisit = _unitOfWork.patientRepo.GetPatientVisitsByPhoneNumber(phoneNumber).ToList();
            */               /* if (patientVisit = ) { return ; }*/
            Patient? patient = _unitOfWork.patientRepo.GetPatientVisitsByPhoneNumber(phoneNumber);
            if (patient == null) { return null; }

            return new GetPatientVisitDto
            {
                Name = patient.Name,

                PatientVisits = patient.PatientVisits.Select(p => new GetPatientVisitsChildDTO
                {
                    DateOfVisit = p.DateOfVisit,
                    Comments = p.Comments,
                    Symptoms = p.Symptoms,
                    VisitStatus = p.VisitStatus,
                    ArrivalTime = p.ArrivalTime,
                    VisitStartTime = p.VisitStartTime,
                    VisitEndTime = p.VisitEndTime,
                    Prescription = p.Prescription


                }).ToList()
            };
        }

        #endregion
        #region Review and Rate
        public bool ReviewAndRate(VisitReviewAndRateDto dto)
        {
            
            PatientVisit? dbVisit = _unitOfWork.visitReviewAndRateRepo.GetById(dto.Id);
            if (dbVisit == null) { return false;}
            dbVisit.Review = dto.Review;
            dbVisit.Rate = dto.Rate;
            _unitOfWork.visitReviewAndRateRepo.Update(dbVisit);
            return true;
        }
        #endregion
    }

}

    
