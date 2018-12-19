using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlassProposalsApp.Dashboard.Interfaces;
using GlassProposalsApp.Data.Models.ViewModels.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace GlassProposalsApp.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AppPolicy")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class DashboardController : Controller
    {
        private readonly IAccountService _accountService;

        public DashboardController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("users")]
        public IActionResult Get()
        {
            return Ok(_accountService.GetAllUsers());
        }
        
        [HttpGet("users/{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_accountService.GetUserById(id));
        }

        [HttpPost("users")]
        public IActionResult Post([FromBody]UserViewModel model)
        {
            return Ok(_accountService.CreateUser(model));
        }

        [HttpPut("users/{id}")]
        public IActionResult Put(Guid id, [FromBody]UserViewModel model)
        {
            return Ok(_accountService.UpdateUser(id, model));
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _accountService.DeleteUser(id);
        }
    }
}
