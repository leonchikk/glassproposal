using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GlassProposalsApp.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GlassProposalsApp.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AppPolicy")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetUserProfileData()
        {
            var indentity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = Guid.Parse(indentity.FindFirst("UserId").Value);

            return Ok(_userService.GetUserProfileData(userId));
        }
    }
}
