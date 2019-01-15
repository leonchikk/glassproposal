using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GlassProposalsApp.Data.ViewModels.Proposals;
using GlassProposalsApp.Application.Interfaces;
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
            var indentity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = Guid.Parse(indentity.FindFirst("UserId").Value);

            return Ok(_proposalService.GetPublicProposals(userId));
        }

        [HttpPost("create/vacation")]
        public IActionResult CreateVacationProposal([FromBody] VacationProposalViewModel model)
        {
            var indentity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = Guid.Parse(indentity.FindFirst("UserId").Value);

            return Ok(_proposalService.CreateVacationProposal(model, userId));
        }

        [HttpPost("create/salary")]
        public IActionResult CreateSalaryProposal([FromBody] SalaryProposalViewModel model)
        {
            var indentity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = Guid.Parse(indentity.FindFirst("UserId").Value);

            return Ok(_proposalService.CreateSalaryIncreaseProposal(model, userId));
        }
        

        [HttpPost("like")]
        public IActionResult LikeProposal([FromBody] UserDecisionViewModel model)
        {
            var indentity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = Guid.Parse(indentity.FindFirst("UserId").Value);

            return Ok(_proposalService.Like(model.ProposalId, userId));
        }

        [HttpPost("dislike")]
        public IActionResult DislikeProposal([FromBody] UserDecisionViewModel model)
        {
            var indentity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = Guid.Parse(indentity.FindFirst("UserId").Value);

            return Ok(_proposalService.Dislike(model.ProposalId, userId));
        }

        [HttpPost("create/levelup")]
        public IActionResult CreateLevelUpProposal([FromBody] LevelUpViewModel model)
        {
            var indentity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = Guid.Parse(indentity.FindFirst("UserId").Value);

            return Ok(_proposalService.CreateLevelUpProposal(model, userId));
        }

        [HttpPost("create/custom")]
        public IActionResult CreateCustomProposal([FromBody] CustomProposalViewModel model)
        {
            var indentity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = Guid.Parse(indentity.FindFirst("UserId").Value);

            return Ok(_proposalService.CreateCustomProposal(model, userId));
        }
    }
}
