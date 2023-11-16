using graduationProject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL
{
    public class GetAllPatientsWithDateDto
    {
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}
