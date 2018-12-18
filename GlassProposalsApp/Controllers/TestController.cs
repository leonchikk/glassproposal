using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlassProposalsApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GlassProposalsApp.API.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private readonly IExportService _exportService; 

        public TestController(IExportService exportService)
        {
            _exportService = exportService;
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var response = _exportService.ExportVacationProposalAsDocx(id);
            return Ok();
        }
    }
}
