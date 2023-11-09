using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graduationProject.DAL
{
    public interface IUnitOfWork
    {
        public IPatientRepo patientRepo { get; }
        public IDoctorRepo doctorRepo { get; }
        public IWeekScheduleRepo weekScheduleRepo { get; }
        public IVisitReviewAndRateRepo visitReviewAndRateRepo { get; }
        public IPatientVisitRepo patientVisitRepo { get; }
        int SaveChanges();
    }
}
