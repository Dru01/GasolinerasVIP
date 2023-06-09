using GasolinerasVIP.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IJWTManagerRepository jWTManager;
        private readonly IUserServiceRepository userServiceRepository;
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UsersController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJWTManagerRepository jWTManager,
            IUserServiceRepository userServiceRepository
        )
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jWTManager = jWTManager;
            this.userServiceRepository = userServiceRepository;
        }

        [HttpPost("SingUp")]
        public async Task<ActionResult> singup([FromBody] UserInfo userInfo)
        {
            var user = new ApplicationUser { UserName = userInfo.username, Email = userInfo.email, FullName = userInfo.fullname };
            var result = await userManager.CreateAsync(user, userInfo.password);
            if (result.Succeeded)
                return Ok();
            else
                return BadRequest(result.Errors);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<Token>> login([FromBody] UserLogin userLogin)
        {
            var ans = await signInManager.PasswordSignInAsync(
                userLogin.username,
                userLogin.password,
                isPersistent:false,
                lockoutOnFailure:false
            );
            if (!ans.Succeeded)
                return BadRequest("Login incorrecto");
            var token = jWTManager.GenerateToken(userLogin.username);
            if (token == null)
            {
                return Unauthorized("Invalid Attempt!");
            }

            // saving refresh token to the db
            UserRefreshToken obj = new UserRefreshToken
            {
                RefreshToken = token.Refresh_Token,
                UserName = userLogin.username
            };

            userServiceRepository.AddUserRefreshToken(obj);
            userServiceRepository.SaveCommit();
            return Ok(token);
        }

        [HttpGet("CurrUserId")]
        [Authorize]
        public async Task<ActionResult<string>> curr_user_id()
        {
            var user = User.Identity;
            if (user == null)
                return NotFound();
            return context.Users.SingleAsync(i => i.UserName == user.Name).Result.Id;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Refresh")]
        public IActionResult Refresh(Token token)
        {
            var principal = jWTManager.GetPrincipalFromExpiredToken(token.Access_Token);
            var username = principal.Identity?.Name;

            //retrieve the saved refresh token from database
            var savedRefreshToken = userServiceRepository.GetSavedRefreshToken(username, token.Refresh_Token);

            if (savedRefreshToken.RefreshToken != token.Refresh_Token)
            {
                return Unauthorized("Invalid attempt!");
            }

            var newJwtToken = jWTManager.GenerateRefreshToken(username);

            if (newJwtToken == null)
            {
                return Unauthorized("Invalid attempt!");
            }

            // saving refresh token to the db
            UserRefreshToken obj = new UserRefreshToken
            {
                RefreshToken = newJwtToken.Refresh_Token,
                UserName = username
            };

            userServiceRepository.DeleteUserRefreshToken(username, token.Refresh_Token);
            userServiceRepository.AddUserRefreshToken(obj);
            userServiceRepository.SaveCommit();

            return Ok(newJwtToken);
        }
    }
}
