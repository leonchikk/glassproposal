using GlassProposalsApp.Data.Entities;
using GlassProposalsApp.Data.Models.ViewModels.Dashboard;
using GlassProposalsApp.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlassProposalsApp.Data.Interfaces
{
    public interface IUsersRepository
    {
        IQueryable<Users> GetDecisionMakersForFirstStage(int processType);
        IQueryable<Users> GetDecisionMakersForNextStage(Guid proposalId);
        Users GetById(Guid id);
        Users CreateUser(UserViewModel model);
        Users Update(Guid id, UserViewModel model);
        IQueryable<Users> GetAll();
        void Remove(Guid id);
    }
}
