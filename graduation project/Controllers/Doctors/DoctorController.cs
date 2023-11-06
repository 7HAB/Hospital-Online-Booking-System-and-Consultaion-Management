using GraduationProject.BL;
using Microsoft.AspNetCore.Mvc;

namespace graduation_project.Controllers.Doctors
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IDoctorManager _doctorManager;

        public DoctorController(IConfiguration configuration,IDoctorManager doctorManager)
        {
            _configuration = configuration;
            _doctorManager = doctorManager;
        }

        #region GetAllDoctors
        [HttpGet]
        public ActionResult<List<GetAllDoctorsDto>?> GetAllDoctors()
        {

            List<GetAllDoctorsDto>? getAllDoctorsDto = _doctorManager.GetAllDoctors();

            if (getAllDoctorsDto == null) { return BadRequest(); }
            // if (getAllDoctorsDto.Count()==0) { return BadRequest(); }

            return _doctorManager.GetAllDoctors();
        }
        #endregion
    }
}
