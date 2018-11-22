﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlassProposalsApp.API.Interfaces;
using GlassProposalsApp.Data.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace GlassProposalsApp.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AppPolicy")]
    public class AccountsController : Controller
    {
        private readonly IAuthorizationService _authorizationService;

        public AccountsController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Post([FromBody]SignInViewModel model)
        {
            return Ok(_authorizationService.Authorizate(model));
        }
    }
}
