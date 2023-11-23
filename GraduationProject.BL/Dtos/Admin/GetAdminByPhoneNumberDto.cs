using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL
{
    public class GetAdminByPhoneNumberDto
    {
        public string? Id {  get; set; }
        public string? PhoneNumber { get; set; }
        public string? Name { get; set; }
        public string? SpecializationName { get; set; }
    }
}
