﻿using AutoMapper;
using GlassProposalsApp.Data;
using GlassProposalsApp.Data.Enumerations;
using GlassProposalsApp.Data.Models;
using GlassProposalsApp.Data.ReponseModels.Proposals;
using GlassProposalsApp.Data.Repositories;
using GlassProposalsApp.Data.ViewModels.Proposals;
using GlassProposalsApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlassProposalsApp.Domain.Services
{
    public class ProposalService : IProposalService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProposalService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ProposalResponseModel CreateCustomProposal(CustomProposalViewModel model, Guid initiatorId)
        {
            var proposal = _unitOfWork.Proposals.CreateCustomProposal(model, initiatorId);
            _unitOfWork.Save();

            return _mapper.Map<Proposals, ProposalResponseModel>(proposal);
        }

        public ProposalResponseModel CreateLevelUpProposal(LevelUpViewModel model, Guid initiatorId)
        {
            var proposal = _unitOfWork.Proposals.CreateLevelUpProposal(model, initiatorId);
            _unitOfWork.Save();

            return _mapper.Map<Proposals, ProposalResponseModel>(proposal);
        }

        public ProposalResponseModel CreateSalaryIncreaseProposal(SalaryProposalViewModel model, Guid initiatorId)
        {
            var proposal = _unitOfWork.Proposals.CreateSalaryIncreaseProposal(model, initiatorId);
            _unitOfWork.Save();

            return _mapper.Map<Proposals, ProposalResponseModel>(proposal);
        }

        public ProposalResponseModel CreateVacationProposal(VacationProposalViewModel model, Guid initiatorId)
        {
            var proposal = _unitOfWork.Proposals.CreateVacationProposal(model, initiatorId);
            _unitOfWork.Save();

            return _mapper.Map<Proposals, ProposalResponseModel>(proposal);
        }

        public IEnumerable<ProposalResponseModel> GetPublicProposals()
        {
            var proposals = _unitOfWork.Proposals.GetPublicProposals();

            return _mapper.Map<IEnumerable<Proposals>, IEnumerable<ProposalResponseModel>>(proposals);
        }

        public IEnumerable<ProposalResponseModel> GetUnhandledProposals(Guid userId)
        {
            var proposals = _unitOfWork.Proposals.GetUnHandledProposalsByUserId(userId);

            return _mapper.Map<IEnumerable<Proposals>, IEnumerable<ProposalResponseModel>>(proposals);
        }

        public IEnumerable<ProposalResponseModel> GetUserProposals(Guid userId)
        {
            var proposals = _unitOfWork.Proposals.GetProposalsByUserId(userId);

            return _mapper.Map<IEnumerable<Proposals>, IEnumerable<ProposalResponseModel>>(proposals);
        }
    }
}
