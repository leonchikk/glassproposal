using GlassProposalsApp.Data.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlassProposalsApp.Application.Interfaces
{
    public interface IUserService
    {
        ProfileResponseModel GetUserProfileData(Guid userId);
    }
}
