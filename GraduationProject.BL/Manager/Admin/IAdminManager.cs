using graduationProject.DAL;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL
{
    public interface IAdminManager
    {
        public Doctor UpdateDoctorById(UpdateDoctorStatusDto updateDoctor, string Id);
        public Doctor ChangeDoctorStatus(string doctorId);

        public void AddSpecialization(AddSpecializationDto? specialization);

        public GetAdminByPhoneNumberDto GetAdminByPhoneNumber(string phoneNumber);
        public List<GetAllSpecializationForAdminDto> GetAllSpecializations();

        Task<Admin> UploadAdminImage(string adminId, IFormFile imageFile);
    }
}
