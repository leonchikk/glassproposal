﻿using GlassProposalsApp.Data.Models;
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
    }
}