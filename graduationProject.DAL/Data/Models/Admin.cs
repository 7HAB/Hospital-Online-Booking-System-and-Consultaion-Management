using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graduationProject.DAL
{
    public class Admin
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [Unique]
        public int Phone { get; set; }
        public string? UserName { get; set; }
        public int? SpecializationId { get; set; } = 0;
        public Specialization? Specialization { get; set; }
        public ICollection<Reception> Receptions { get; set; } = new List<Reception>();
    }
}
