using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graduationProject.DAL
{
    public class PatientVisitRepo : IPatientVisitRepo
    {
        private readonly HospitalContext _context;
        public PatientVisitRepo(HospitalContext context)
        {
                _context = context; 
        }

        public void AddPatientVisit(PatientVisit patientVisit)
        {
            _context.Set<PatientVisit>().Add(patientVisit);
        }
    }
}
