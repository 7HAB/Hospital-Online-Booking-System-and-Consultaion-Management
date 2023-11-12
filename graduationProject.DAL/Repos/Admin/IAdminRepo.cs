using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graduationProject.DAL
{
    public interface IAdminRepo
    {
        public Doctor? UpdateDoctorById(string doctorId);
    }
}
