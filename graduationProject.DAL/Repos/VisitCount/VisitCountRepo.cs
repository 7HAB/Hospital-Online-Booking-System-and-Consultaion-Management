using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graduationProject.DAL
{
    public class VisitCountRepo : IVisitCountRepo
    {
        HospitalContext _Context;

        public VisitCountRepo(HospitalContext context)
        {
              _Context = context;
        }
   
        public void AddVisitCount(VisitCount visitCount)
        {
            _Context.Set<VisitCount>().Add(visitCount);
        }

        public int GetCount(DateTime date , string DoctorId) 
        {
            return _Context.Set<VisitCount>().Where(d => d.DoctorId == DoctorId && d.Date.Date == date.Date).Count();
        }
    }
}
