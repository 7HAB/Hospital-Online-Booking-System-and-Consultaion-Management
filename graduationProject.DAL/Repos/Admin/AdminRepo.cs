﻿using graduationProject.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Doctor? doctorToUpdate = _context.Set<Doctor>().FirstOrDefault(d => d.Id == doctorId);

            if (doctorToUpdate != null)
            {
                 _context.Set<Doctor>().Update(doctorToUpdate);
                
            }
            return doctorToUpdate;
        }

        #region update week schedule record
        public void UpdateWeekScheduleRecord (WeekSchedule schedule)
        {

            _context.Set<WeekSchedule>().Update(schedule);
            _context.SaveChanges();
        }
        #endregion

        #region get week schedule record by id
        public WeekSchedule? GetWeekScheduleById(int id)
        {
            return _context.Set<WeekSchedule>().FirstOrDefault(w => w.Id == id);

        }
        #endregion
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
        
        #region add week schedule
        public void AddWeekSchedule(WeekSchedule schedule)
        {

            _context.Set<WeekSchedule>().Add(schedule);
            _context.SaveChanges();
        }
        #endregion
    }

}
