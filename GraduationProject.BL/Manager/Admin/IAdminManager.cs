using graduationProject.DAL;
using graduationProject.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL
{
    public interface IAdminManager
    {
        public Doctor UpdateDoctorById(UpdateDoctorStatusDto updateDoctor, string id);
        public void AddWeekSchedule(AddWeekScheduleDto addWeekSchedule);
        public GetDoctorByIDForAdminDto GetDoctorByIdForAdmin(string id);

        public Doctor ChangeDoctorStatus(string doctorId);

        public void AddSpecialization(AddSpecializationDto? specialization);

        public GetAdminByPhoneNumberDto GetAdminByPhoneNumber(string phoneNumber);
        public List<GetAllSpecializationForAdminDto> GetAllSpecializations();
        public WeekScheduleForDoctorsDto GetWeekScheduleById(int id);
        public WeekSchedule UpdateWeekScheduleRecord(WeekScheduleForDoctorsDto weekSchedule, int id);


        public GetAllPatientsWithDateDto UpdateArrivedPatientStatus(UpdateArrivalPatientStatusDto updateArrivalPatientStatusDto);
    }
}
