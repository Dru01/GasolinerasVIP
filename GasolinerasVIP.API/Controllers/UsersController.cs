using GasolinerasVIP.API.Models;
using GuiaDCEA.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GasolinerasVIP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public UsersController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager
        )
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost("singup")]
        public async Task<ActionResult> singup([FromBody] UserInfo userInfo)
        {
            var user = new IdentityUser { UserName = userInfo.username, Email = userInfo.email };
            var result = await userManager.CreateAsync(user, userInfo.password);
            if (result.Succeeded)
                return Ok();
            else
                return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> login([FromBody] UserLogin userLogin)
        {
            var resul = await signInManager.PasswordSignInAsync(
                userLogin.username,
                userLogin.password,
                isPersistent:false,
                lockoutOnFailure:false
            );
            if (resul.Succeeded)
            {
                return GetUserToken(userLogin);
            }
            else
            {
                return BadRequest("Login incorrecto");
            }
        }

        private UserToken GetUserToken(UserLogin userLogin)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userLogin.username)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("iuash129diuha.osadqw-/AAQDsibsqw12912")
            );
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(5);

            var token = new JwtSecurityToken(
                issuer: "GasolinerasVIP.API",
                audience: "GasolinerasVIP.API",
                claims:  claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new UserToken()
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expires = expires
            };
        }
    }
}
