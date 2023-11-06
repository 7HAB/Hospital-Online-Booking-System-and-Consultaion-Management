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



    }
}
