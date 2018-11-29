using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GlassProposalsApp.Data.ViewModels.Proposals;
using GlassProposalsApp.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GlassProposalsApp.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AppPolicy")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ProposalsController : Controller
    {
        private readonly IProposalService _proposalService;

        public ProposalsController(IProposalService proposalService)
        {
            _proposalService = proposalService;
        }

        [HttpGet]
        public IActionResult GetUsersProposals()
        {
            var indentity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = Guid.Parse(indentity.FindFirst("UserId").Value);

            return Ok(_proposalService.GetUserProposals(userId));
        }

        [HttpGet("unhandled")]
        public IActionResult GetUnhandledProposals()
        {
            var indentity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = Guid.Parse(indentity.FindFirst("UserId").Value);

            return Ok(_proposalService.GetUnhandledProposals(userId));
        }

        [HttpGet("public")]
        public IActionResult GetPublicProposals()
        {
            return Ok(_proposalService.GetPublicProposals());
        }

        [HttpPost("create")]
        public IActionResult CreateProposal([FromBody] ProposalViewModel model)
        {
            var indentity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = Guid.Parse(indentity.FindFirst("UserId").Value);

            return Ok(_proposalService.Create(model, userId));
        }
    }
}
