using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL
{
    public interface IPatientManager
    {
        // public List<GetAllDoctorsDto> GetAllDoctors();
        public GetPatientByPhoneDTO getPatientByPhoneDTO(string phoneNumber);


    }

}
