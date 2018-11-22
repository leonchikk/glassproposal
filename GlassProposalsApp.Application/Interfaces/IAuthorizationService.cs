using GlassProposalsApp.Data.ReponseModels;
using GlassProposalsApp.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlassProposalsApp.API.Interfaces
{
    public interface IAuthorizationService
    {
        SignInResponseModel Authorizate(SignInViewModel model);
    }
}
