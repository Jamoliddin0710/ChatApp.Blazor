using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly MessageService _messageService;
        public AccountController(SignInManager<IdentityUser> signInManager , MessageService messageService)
        {
            _messageService = messageService;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username)
        {
            var user = new IdentityUser(username);
            await _signInManager.SignInAsync(user, isPersistent: true);   // user ro'yxatdan o'tkazib token genrate qiladi va cookiega yozadi
            return Ok(user);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var username = User.FindFirst(ClaimTypes.Name)!.Value;
            return Ok("username " + username);
        }
        [HttpGet("groups")]
        public async Task<IActionResult> GetGroups() =>
            Ok(new List<string>
            {
                "Dotnet",
                "Java",
                "Php"
            });

        [HttpGet("{group}")]
        public async Task<IActionResult> GetMessage(string group) =>
            Ok(_messageService.Messages[group].Select(mes=>$"{mes.Item1}:{mes.Item2}").ToList());
    }
}
