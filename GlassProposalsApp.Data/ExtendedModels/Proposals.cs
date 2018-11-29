using GlassProposalsApp.Data.ViewModels.Proposals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlassProposalsApp.Data.Models
{
    public partial class Proposals
    {
        public Proposals(Processes process, Guid initiatorId, ProposalViewModel model)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            InitiatorId = initiatorId;
            Description = model.Description;
            IsClosed = false;
            IsUrgently = model.IsUrgently;
            ProcessId = process.Id;
            Title = model.Title;
            CurrentStageId = process.Stages.First()?.Id;
        }
    }
}
