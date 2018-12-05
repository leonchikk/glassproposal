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
    public class DecisionsController : Controller
    {
        private readonly IDecisionService _decisionService;

        public DecisionsController(IDecisionService decisionService)
        {
            _decisionService = decisionService;
        }

        [HttpPost("approve")]
        public IActionResult Approve([FromBody] ApproveProposalViewModel model)
        {
            var indentity = HttpContext.User.Identity as ClaimsIdentity;
            var decisionMakerId = Guid.Parse(indentity.FindFirst("UserId").Value);

            _decisionService.ApproveProposal(model.ProposalId, decisionMakerId, model.NextDecisionMakerId);
            return Ok();
        }

        [HttpPost("reject")]
        public IActionResult Reject([FromBody] RejectProposalViewModel model)
        {
            var indentity = HttpContext.User.Identity as ClaimsIdentity;
            var decisionMakerId = Guid.Parse(indentity.FindFirst("UserId").Value);

            _decisionService.RejectProposal(model.ProposalId, decisionMakerId, model.Reason);
            return Ok();
        }

        [HttpGet("stages/first/{proccesType}")]
        public IActionResult Get(int proccesType)
        {
            return Ok(_decisionService.GetDecisionMakersForFirstStage(proccesType));
        }

        [HttpGet("stages/next/{proposalId}")]
        public IActionResult Get(Guid proposalId)
        {
            return Ok(_decisionService.GetDecisionMakersForNextStage(proposalId));
        }
    }
}
