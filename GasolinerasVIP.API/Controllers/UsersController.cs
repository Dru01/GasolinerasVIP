using GasolinerasVIP.API.Models;
using GuiaDCEA.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GasolinerasVIP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public UsersController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
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

        //[HttpPost("login")]
        //public async Task<ActionResult> login([FromBody] UserInfo userInfo)
        //{
        //    var result = 
        //}
    }
}
