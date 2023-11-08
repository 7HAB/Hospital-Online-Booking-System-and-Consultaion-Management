using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graduationProject.DAL
{
    public class VisitReviewAndRateRepo : IVisitReviewAndRateRepo
    {
        private readonly HospitalContext _context;

        public VisitReviewAndRateRepo(HospitalContext context) 
        {
            _context = context;
        }
        public void Update(PatientVisitsWithDoctor entity)
        {
            _context.Set<PatientVisitsWithDoctor>().Update(entity);
        }
        public PatientVisitsWithDoctor? GetById(int? id)
        {
            return _context.Set<PatientVisitsWithDoctor>().FirstOrDefault(d => d.Id == id);
        }
    }
}
