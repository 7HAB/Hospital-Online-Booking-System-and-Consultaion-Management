using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graduationProject.DAL
{
    public interface IVisitCountRepo
    {
        public void AddVisitCount(VisitCount visitCount);

        public int GetCount(DateTime date, string DoctorId);
    }
}
