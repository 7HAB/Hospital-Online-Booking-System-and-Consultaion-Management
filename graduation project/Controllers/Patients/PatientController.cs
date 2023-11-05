using graduationProject.DAL;
using GraduationProject.BL.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace graduation_project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<Patient> _userManager;
        public PatientController(IConfiguration configuration, 
            UserManager<Patient> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        #region Login

        [HttpPost]
        [Route("login")]
        //=> /api/users/static-login
        public async Task<ActionResult<TokenDto>> Login(LoginDto credentials)
        {
            #region Username and Password verification
            
            Patient? user = await _userManager.FindByNameAsync(credentials.PhoneNumber);

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

        #region Register

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            var user = new Patient
            {
                Name = registerDto.Name
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.PhoneNumber,
                Gender = registerDto.Gender,
                DateOfBirth = registerDto.DateOfBirth,
                
            };
            var creationResult = await _userManager.CreateAsync(user, registerDto.Password);
            if (!creationResult.Succeeded)
            {
                return BadRequest(creationResult.Errors);
            }

            var claimsList = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Role, "Patient"),
            new Claim(ClaimTypes.Name, user.UserName)
        //    new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
        };
            await _userManager.AddClaimsAsync(user, claimsList);

            return Ok();
        }

        #endregion
    }
}
