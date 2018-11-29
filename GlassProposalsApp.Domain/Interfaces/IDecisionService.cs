using GlassProposalsApp.Data.ReponseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlassProposalsApp.Domain.Interfaces
{
    public interface IDecisionService
    {
        IEnumerable<UserResponseModel> GetDecisionMakersForFirstStage(int processType);
        IEnumerable<UserResponseModel> GetDecisionMakersForNextStage(Guid proposalId);
    }
}
