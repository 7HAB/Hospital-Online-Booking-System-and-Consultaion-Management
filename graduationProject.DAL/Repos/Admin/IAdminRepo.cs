using graduationProject.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graduationProject.DAL
{
    public interface IAdminRepo
    {
        public Doctor? UpdateDoctorById(string doctorId);
        public Doctor? ChangeDoctorStatus(string doctorId);

        public void AddSpecialization(Specialization? specialization);
        public Admin? GetAdminByPhoneNumber(string PhoneNumber);
        public Specialization GetSpecializationByAdmin(int? id);
        public void AddWeekSchedule(WeekSchedule schedule);

        public void UpdateWeekScheduleRecord(WeekSchedule schedule);

        public WeekSchedule? GetWeekScheduleById(int id);

        public PatientVisit UpdateArrivedPatientStatus(PatientVisit patientVisit);
        public PatientVisit GetVisit(int id);
        public void UpdateAdminByPhone(Admin admin);

        public Reception? GetReceptionByPhoneNumber(string PhoneNumber);
    }
}
