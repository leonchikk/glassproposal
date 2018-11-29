using GlassProposalsApp.Data.ReponseModels.Accounts;
using GlassProposalsApp.Data.ViewModels.Accounts;
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
