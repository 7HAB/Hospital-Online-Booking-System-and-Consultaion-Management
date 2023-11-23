using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace graduationProject.DAL
{
    public class AdminRepo : IAdminRepo
    {
        private readonly HospitalContext _context;
        public AdminRepo(HospitalContext context)
        {
              _context = context;
        }

        public Admin? GetAdminByPhoneNumber(string PhoneNumber)
        {
            return _context.Set<Admin>().FirstOrDefault(A => A.PhoneNumber == PhoneNumber);
            
        }
        public Specialization GetSpecializationByAdmin(int? id)
        {
            return _context.Set<Specialization>().Find(id)!;
        }

        public Doctor? UpdateDoctorById(string doctorId)
        {
            Doctor? doctorToUpdate = _context.Set<Doctor>().Include(A => A.specialization).FirstOrDefault(d => d.Id == doctorId);

            if (doctorToUpdate != null)
            {
                 _context.Set<Doctor>().Update(doctorToUpdate);
                
            }
            return doctorToUpdate;
        }

        public Doctor? ChangeDoctorStatus(string doctorId)
        {
            Doctor? doctorToUpdate = _context.Set<Doctor>().FirstOrDefault(d => d.Id == doctorId);
            if (doctorToUpdate != null)
            {
                _context.Set<Doctor>().Update(doctorToUpdate);

            }
            return doctorToUpdate;

        }

        public void AddSpecialization(Specialization? specialization) 
        {
            if (specialization == null)
            {
                throw new ArgumentNullException(nameof(specialization), "Specialization cannot be null.");
            }

            _context.Set<Specialization>().Add(specialization);

        }

        public void UploadAdminImage(Admin admin)
        {
            {
                var existingAdmin = _context.Set<Admin>().Find(admin.Id);
                DeleteImage(existingAdmin.StoredFileName);

                existingAdmin.FileName = admin.FileName;
                existingAdmin.StoredFileName = admin.StoredFileName;
                existingAdmin.ContentType = admin.ContentType;


                _context.SaveChanges();
            }

        }

        public void DeleteImage(string storedFileName)
        {
            if (storedFileName == null)
            {
                return;
            }

            var imagePath = Path.Combine("AdminImages", storedFileName);
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath);

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }
    }
}
