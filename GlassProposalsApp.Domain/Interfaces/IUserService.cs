using GlassProposalsApp.Data.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlassProposalsApp.Domain.Interfaces
{
    public interface IUserService
    {
        ProfileResponseModel GetUserProfileData(Guid userId);
    }
}
