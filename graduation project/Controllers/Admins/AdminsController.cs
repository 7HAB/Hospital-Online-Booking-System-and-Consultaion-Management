using graduationProject.DAL;
using GraduationProject.BL;
using GraduationProject.BL.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace graduation_project.Controllers.Admins
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAdminManager _adminManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AdminsController(IConfiguration configuration,
            UserManager<IdentityUser> userManager, IAdminManager adminManager, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _userManager = userManager;
            _adminManager = adminManager;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }
        #region Get Admin By Phone Number
        [HttpGet]
        [Route("Admin/{phoneNumber}")]
        public ActionResult<GetAdminByPhoneNumberDto> GetAdminByPhoneNumber(string phoneNumber)
        {
            GetAdminByPhoneNumberDto? Admin = _adminManager.GetAdminByPhoneNumber(phoneNumber);
            if(Admin == null) { return NotFound(); }
            var baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}";

            baseUrl = baseUrl.TrimEnd('/');

            var imageUrl = $"{baseUrl}/{Admin.ImageStoredFileName}";
            imageUrl = imageUrl.Replace("wwwroot/", string.Empty);
            Admin.ImageUrl = imageUrl;


            return Admin;
        }
        #endregion
        #region get All specializations and doctors for admin
        [HttpGet]
        [Route("Specializations")]
        public ActionResult<List<GetAllSpecializationForAdminDto>> GetAllSpecializationForAdmins()
        {
            return _adminManager.GetAllSpecializations();
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
                return NotFound("User not found");
            }

            bool isPasswordCorrect = await _userManager.CheckPasswordAsync(user, credentials.Password);
            if (!isPasswordCorrect)
            {
                //  return Unauthorized();
                return Unauthorized("Invalid password");
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
                expires: DateTime.Now.AddMinutes(720));
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

        #region Update Doctor With Id
        [HttpPut]
        [Route("admins/updatedoctor/{doctorId}")]
        public IActionResult UpdateDoctorById(UpdateDoctorStatusDto updateDoctor , string doctorId)
        {
            Doctor? doctor = _adminManager.UpdateDoctorById(updateDoctor , doctorId);
            if (doctor == null)
            {
                return Content("Null Record");
            }
            return Content("Updated");
        }
        #endregion
        #region ChangeStatus
        [HttpPut("admins/changedoctorstatus/{doctorId}")]
        public IActionResult ChangeStatus(string doctorId)
        {
            Doctor? doctor = _adminManager.ChangeDoctorStatus(doctorId);

            if (doctor == null)
            {
                return NotFound("Doctor not found");
            }

            return NoContent(); 
        }

        #endregion
        #region AddSpecialization
        [HttpPost]
        [Route("Admins/AddSpecialization")]
        public IActionResult AddSpecialization(AddSpecializationDto addSpecializationDto)
        {
            _adminManager.AddSpecialization(addSpecializationDto);
            return StatusCode(StatusCodes.Status201Created);

        }

        #endregion

        #region UploadImages

        [HttpPost]
        // [Authorize(Policy = "DoctorPolicy")]
        [Route("admins/uploadimage/{adminId}")]
        public async Task<IActionResult> UploadImage(string adminId, IFormFile imageFile)
        {
            try
            {
                Admin uploadedAdmin = await _adminManager.UploadAdminImage(adminId, imageFile);

                if (uploadedAdmin != null)
                {
                    return Ok(uploadedAdmin);
                }
                else
                {
                    return BadRequest("No file provided or an error occurred during upload.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        #endregion

    }
}
