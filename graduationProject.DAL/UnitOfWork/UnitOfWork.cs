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

        public UnitOfWork(HospitalContext context, IPatientRepo PatientRepo)
        {
            _context = context;
            patientRepo = PatientRepo;
        }
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
