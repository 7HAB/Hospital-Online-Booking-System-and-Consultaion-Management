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
        void UploadAdminImage(Admin admin);

    }
}
