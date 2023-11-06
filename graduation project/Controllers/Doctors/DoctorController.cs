using graduationProject.DAL;
using GraduationProject.BL;
using GraduationProject.BL.Dtos.Doctor;
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
        #region GetDoctorBySpecification
        [HttpGet]
        [Route("{id}")]
        public ActionResult<List<GetDoctorsBySpecializationDto>> GetBySpecialization(int id)
        {
            List<GetDoctorsBySpecializationDto> DoctorWithSpecialization = _doctorManager.GetDoctorsBySpecialization(id);
            if (DoctorWithSpecialization is null)
                return NotFound();

            return DoctorWithSpecialization;
        }
        #endregion
    }
}
