using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graduationProject.DAL
{
    public class UnitOfWork : IUnitOfWork
    { private readonly HospitalContext _context;

        public IPatientRepo patientRepo {  get;  }
        public IDoctorRepo doctorRepo { get; }
        public IVisitReviewAndRateRepo visitReviewAndRateRepo { get; }

        public IWeekScheduleRepo weekScheduleRepo { get; }

        public UnitOfWork(HospitalContext context, IPatientRepo PatientRepo, IDoctorRepo DoctorRepo, IWeekScheduleRepo WeekScheduleRepo)
        {
            _context = context;
            patientRepo = PatientRepo;
            doctorRepo = DoctorRepo;
            this.visitReviewAndRateRepo = visitReviewAndRateRepo;
            weekScheduleRepo = WeekScheduleRepo;
        }
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
