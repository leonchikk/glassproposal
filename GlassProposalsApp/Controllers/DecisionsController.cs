using System;
using System.Collections.Generic;
using System.Linq;
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
    public class DecisionsController : Controller
    {
        private readonly IDecisionService _decisionService;

        public DecisionsController(IDecisionService decisionService)
        {
            _decisionService = decisionService;
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
