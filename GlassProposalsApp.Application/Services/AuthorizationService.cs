using GlassProposalsApp.API.Interfaces;
using GlassProposalsApp.Data;
using GlassProposalsApp.Data.ReponseModels.Accounts;
using GlassProposalsApp.Data.ViewModels.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlassProposalsApp.Application.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly GlassProposalContext _dbContext;
        private readonly ITokenService _tokenService;

        public AuthorizationService(GlassProposalContext dbContext, ITokenService tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }

        public SignInResponseModel Authorizate(SignInViewModel model)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

            if (user == null)
                throw new Exception("User with such email doesn't exist");

            return new SignInResponseModel
            {
                UserId = user.Id,
                SecurityLevel = user.SecurityLevel,
                Token = _tokenService.Generate(user.Email, user.SecurityLevel, user.Id)
            };
        }
    }
}
