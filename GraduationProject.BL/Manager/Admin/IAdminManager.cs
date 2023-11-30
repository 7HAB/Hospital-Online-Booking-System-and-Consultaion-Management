using graduationProject.DAL;
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

        public List<GetTopRatedDoctorsDto> GetAverageRateForEachDoctor();

        public int GetNumberOfPatientsForADay(DateTime date);

        public int GetNumberOfAvailableDoctorInADay(DateTime date);

        public int GetNumberOfPatientsForAPeriod(DateTime startDate, DateTime endDate);

        public List<PatientVisit> GetPatientVisitsInAPeriodAndSpecialization(DateTime startDate, DateTime endDate, int specializationId);
        public List<GetDoctorsVisitsNumberDto> GetDoctorsPatientVisitsNumber();


    }
}
