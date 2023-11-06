using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graduationProject.DAL
{
    public class PatientRepo : GenaricRepo<Patient>, IPatientRepo
    {
        private readonly HospitalContext _context;

        public PatientRepo(HospitalContext context) : base(context)
        {
            _context = context;
        }

        #region GetPatientByPhone
        public Patient? GetPatientByPhoneNumber(string phoneNumber)
        {
            return _context.Set<Patient>().FirstOrDefault(x => x.PhoneNumber == phoneNumber);
        }

        #endregion

        #region GetMedicalHistoryByPhoneNumber
        public MedicaHistory? GetMedicaHistoryByPhoneNumber(string phoneNumber)
        {
            Patient? patient = _context.Set<Patient>().FirstOrDefault(p => p.PhoneNumber == phoneNumber);
            if (patient == null) { return null; }
            return patient.MedicaHistory;
        }

        #endregion
        #region GetPatientVisitsByPhoneNumber
        public Patient GetPatientVisitsByPhoneNumber(string phoneNumber)
        {
            Patient? patient = _context.Set<Patient>().FirstOrDefault(p => p.PhoneNumber == phoneNumber);
            if (patient == null) { return null; }
            return patient;
        }
        #endregion


    }
}
