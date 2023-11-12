using graduationProject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraduationProject.BL.Dtos.Doctor;

namespace GraduationProject.BL
{
    public interface IDoctorManager
    {
        public List<GetAllDoctorsDto> GetAllDoctors();
        public GetDoctorByIDDto GetDoctorBYId(string id);
        public List<GetDoctorsBySpecializationDto> GetDoctorsBySpecialization(int id);
        public GetAllWeekScheduleDto? GetAllWeekScheduleByDoctorId(string id);
        public bool UpdatePatientVisit(UpdatePatientVisitDto updateDto);

        public List<GetAllPatientsWithDateDto> GetAllPatientsWithDate(DateTime date, string DoctorId);
    }
}
