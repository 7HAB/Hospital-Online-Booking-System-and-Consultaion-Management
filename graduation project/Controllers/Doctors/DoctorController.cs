using graduationProject.DAL;
using GraduationProject.BL;
using GraduationProject.BL.Dtos;
using GraduationProject.BL.Dtos.Doctor;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace graduation_project.Controllers.Doctors
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IDoctorManager _doctorManager;

        public DoctorController(IConfiguration configuration,
            UserManager<IdentityUser> userManager, IDoctorManager doctorManager)
        {
            _configuration = configuration;
            _userManager = userManager;
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
        #region GetDoctorById
        [HttpGet]
        [Route("doctors/{DoctorId}")]
        public ActionResult<GetDoctorByIDDto> GetDoctorById(string DoctorId)
        {
            GetDoctorByIDDto? GetDOctorById = _doctorManager.GetDoctorBYId(DoctorId);
            if (GetDOctorById == null)
                return NotFound();
            return GetDOctorById;
        }
        #endregion
        #region GetDoctorBySpecification
        [HttpGet]
        [Route("doctors/specialization/{id}")]
        public ActionResult<List<GetDoctorsBySpecializationDto>> GetBySpecialization(int id)
        {
            List<GetDoctorsBySpecializationDto> DoctorWithSpecialization = _doctorManager.GetDoctorsBySpecialization(id);
            if (DoctorWithSpecialization is null)
                return NotFound();

            return DoctorWithSpecialization;
        }
        #endregion
        #region doctor Login

        [HttpPost]
        [Route("Doctor/login")]
        //=> /api/users/static-login
        public async Task<ActionResult<TokenDto>> Login(LoginDto credentials)
        {
            #region Username and Password verification

            IdentityUser? user = await _userManager.FindByNameAsync(credentials.PhoneNumber);

            if (user is null)
            {
                return Content("null");
            }

            bool isPasswordCorrect = await _userManager.CheckPasswordAsync(user, credentials.Password);
            if (!isPasswordCorrect)
            {
                //  return Unauthorized();
                return Content("password wrong");
            }

            #endregion

            #region Generate Token

            var claimsList = await _userManager.GetClaimsAsync(user);
            string secretKey = _configuration.GetValue<string>("SecretKey")!;
            var algorithm = SecurityAlgorithms.HmacSha256Signature;

            var keyInBytes = Encoding.ASCII.GetBytes(secretKey);
            var key = new SymmetricSecurityKey(keyInBytes);
            var signingCredentials = new SigningCredentials(key, algorithm);

            var token = new JwtSecurityToken(
                claims: claimsList,
                signingCredentials: signingCredentials,
                expires: DateTime.Now.AddMinutes(10));
            var tokenHandler = new JwtSecurityTokenHandler();

            return new TokenDto
            {
                Token = tokenHandler.WriteToken(token),
            };

            #endregion

        }
        #endregion
        #region doctor Register

        [HttpPost]
        [Route("Doctor/register")]
        public async Task<ActionResult> Register(RegisterDoctorDto registerDto)
        {
            var user = new Doctor
            {

                Name = registerDto.Name,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.PhoneNumber,
                DateOfBirth = registerDto.DateOfBirth,
                Title = registerDto.Title,
                Description = registerDto.Description,
                Salary = registerDto.Salary,
                AssistantID = registerDto.AssistantID,
                AssistantPhoneNumber = registerDto.AssistantPhoneNumber,
                AssistantDateOfBirth = registerDto.AssistantDateOfBirth,
                SpecializationId = registerDto.SpecializationId
            };
            var creationResult = await _userManager.CreateAsync(user, registerDto.Password);
            if (!creationResult.Succeeded)
            {
                return BadRequest(creationResult.Errors);
            }

            var claimsList = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Role, "Doctor"),
            new Claim(ClaimTypes.Name, user.UserName)
        //    new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
        };
            await _userManager.AddClaimsAsync(user, claimsList);

            return Ok();
        }

        #endregion
        #region ReceptionLogin

        [HttpPost]
        [Route("reception/login")]
        //=> /api/users/static-login
        public async Task<ActionResult<TokenDto>> ReceptionLogin(LoginDto credentials)
        {
            #region Username and Password verification

            IdentityUser? user = await _userManager.FindByNameAsync(credentials.PhoneNumber);

            if (user is null)
            {
                return Content("null");
            }

            bool isPasswordCorrect = await _userManager.CheckPasswordAsync(user, credentials.Password);
            if (!isPasswordCorrect)
            {
                //  return Unauthorized();
                return Content("password wrong");
            }

            #endregion

            #region Generate Token

            var claimsList = await _userManager.GetClaimsAsync(user);
            string secretKey = _configuration.GetValue<string>("SecretKey")!;
            var algorithm = SecurityAlgorithms.HmacSha256Signature;

            var keyInBytes = Encoding.ASCII.GetBytes(secretKey);
            var key = new SymmetricSecurityKey(keyInBytes);
            var signingCredentials = new SigningCredentials(key, algorithm);

            var token = new JwtSecurityToken(
                claims: claimsList,
                signingCredentials: signingCredentials,
                expires: DateTime.Now.AddMinutes(10));
            var tokenHandler = new JwtSecurityTokenHandler();

            return new TokenDto
            {
                Token = tokenHandler.WriteToken(token),
            };

            #endregion

        }

        #endregion
        #region ReceptionRegister

        [HttpPost]
        [Route("reception/register")]
        public async Task<ActionResult> ReceptionRegister(ReceptionRegisterDto registerDto)
        {
            var user = new Reception
            {

                Name = registerDto.Name,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.PhoneNumber,

            };
            var creationResult = await _userManager.CreateAsync(user, registerDto.Password);
            if (!creationResult.Succeeded)
            {
                return BadRequest(creationResult.Errors);
            }

            var claimsList = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Role, "Reception"),
            new Claim(ClaimTypes.Name, user.UserName)
        //    new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
        };
            await _userManager.AddClaimsAsync(user, claimsList);

            return Ok();
        }
        #endregion

        #region admin login
        [HttpPost]
        [Route("Admins/login")]
        //=> /api/users/static-login
        public async Task<ActionResult<TokenDto>> AdminLogin(LoginDto credentials)
        {
            #region Username and Password verification

            IdentityUser? user = await _userManager.FindByNameAsync(credentials.PhoneNumber);

            if (user is null)
            {
                return Content("null");
            }

            bool isPasswordCorrect = await _userManager.CheckPasswordAsync(user, credentials.Password);
            if (!isPasswordCorrect)
            {
                //  return Unauthorized();
                return Content("password wrong");
            }

            #endregion

            #region Generate Token

            var claimsList = await _userManager.GetClaimsAsync(user);
            string secretKey = _configuration.GetValue<string>("SecretKey")!;
            var algorithm = SecurityAlgorithms.HmacSha256Signature;

            var keyInBytes = Encoding.ASCII.GetBytes(secretKey);
            var key = new SymmetricSecurityKey(keyInBytes);
            var signingCredentials = new SigningCredentials(key, algorithm);

            var token = new JwtSecurityToken(
                claims: claimsList,
                signingCredentials: signingCredentials,
                expires: DateTime.Now.AddMinutes(10));
            var tokenHandler = new JwtSecurityTokenHandler();

            return new TokenDto
            {
                Token = tokenHandler.WriteToken(token),
            };

            #endregion

        }
        #endregion
        #region admin register

        [HttpPost]
        [Route("Admins/register")]
        public async Task<ActionResult> AdminRegister(RegisterAdminDto registerDto)
        {
            var user = new Admin
            {

                Name = registerDto.Name,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.PhoneNumber,
                SpecializationId = registerDto.SpecializationId
            };
            var creationResult = await _userManager.CreateAsync(user, registerDto.Password);
            if (!creationResult.Succeeded)
            {
                return BadRequest(creationResult.Errors);
            }

            var claimsList = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim(ClaimTypes.Name, user.UserName)
        //    new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
        };
            await _userManager.AddClaimsAsync(user, claimsList);

            return Ok();
        }

        #endregion
    }

}