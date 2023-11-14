using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Dtos.VisitCount
{
    internal class UpateVisitCountDto
    {
        public int Id { get; set; }
        public int ActualNoOfPatients { get; set; }
        public int LimitOfPatients { get; set; } 
    }
}
