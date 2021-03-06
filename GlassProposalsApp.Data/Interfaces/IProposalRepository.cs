﻿using GlassProposalsApp.Data.Entities;
using GlassProposalsApp.Data.ViewModels.Proposals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlassProposalsApp.Data.Interfaces
{
    public interface IProposalRepository
    {
        Proposals GetById(Guid Id);
        IQueryable<Proposals> GetPublicProposals();
        IQueryable<Proposals> GetUnHandledProposalsByUserId(Guid userId);
        IQueryable<Proposals> GetProposalsByUserId(Guid userId);
        Proposals CreateCustomProposal(CustomProposalViewModel model, Guid initiatorId);
        Proposals CreateLevelUpProposal(LevelUpViewModel model, Guid initiatorId);
        Proposals CreateSalaryIncreaseProposal(SalaryProposalViewModel model, Guid initiatorId);
        Proposals CreateVacationProposal(VacationProposalViewModel model, Guid initiatorId);
        void Like(Guid proposalId, Guid userId);
        void Dislike(Guid proposalId, Guid userId);
        void Approve(Guid proposalId, Guid changeInitiatorId, Guid? nextDecisionMakerId);
        void Reject(Guid proposalId, Guid changeInitiatorId, string reason);
    }
}
