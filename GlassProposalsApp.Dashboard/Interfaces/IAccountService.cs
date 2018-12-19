using GlassProposalsApp.Data.Models.ViewModels.Dashboard;
using GlassProposalsApp.Data.ReponseModels.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GlassProposalsApp.Dashboard.Interfaces
{
    public interface IAccountService
    {
        IEnumerable<UserResponseModel> GetAllUsers();
        UserResponseModel CreateUser(UserViewModel model);
        UserResponseModel UpdateUser(Guid id, UserViewModel model);
        UserResponseModel GetUserById(Guid id);
        void DeleteUser(Guid id);
    }
}
