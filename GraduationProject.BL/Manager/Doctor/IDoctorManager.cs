using GraduationProject.BL;
using graduationProject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL
{
    public interface IDoctorManager
    {
        public List<GetAllDoctorsDto> GetAllDoctors();

    }
}
