using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graduationProject.DAL
{
    public class VisitCountRepo : IVisitCountRepo
    {
        HospitalContext _context;

        public VisitCountRepo(HospitalContext context)
        {
              _context = context;
        }
   
        /*public void AddVisitCount(VisitCount visitCount)
        {
            _context.Set<VisitCount>().Add(visitCount);
        }*/

        public int GetCount(DateTime date , string DoctorId) 
        {
            return _context.Set<VisitCount>().Where(d => d.DoctorId == DoctorId && d.Date.Date == date.Date).Count();
        }
        public VisitCount? AddOrUpdateVisitCount(int Id)
        {
            VisitCount? visitCount = _context.Set<VisitCount>().FirstOrDefault(v => v.Id == Id);

            if (visitCount != null)
            {
                _context.Set<VisitCount>().Update(visitCount);
            }
            else
            {
                _context.Set<VisitCount>().Add(visitCount);
            }
            return visitCount;
        }
    }
}
