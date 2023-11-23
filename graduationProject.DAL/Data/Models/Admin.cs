using Microsoft.AspNetCore.Identity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graduationProject.DAL
{
    public class Admin : IdentityUser
    {
        public string? Name { get; set; }
        public int? SpecializationId { get; set; } = 0;
        #region uploadImages
        public string? FileName { get; set; }
        public string? StoredFileName { get; set; }
        public string? ContentType { get; set; }
        #endregion

        public Specialization? Specialization { get; set; }
        public ICollection<Reception> Receptions { get; set; } = new List<Reception>();
    }
}
