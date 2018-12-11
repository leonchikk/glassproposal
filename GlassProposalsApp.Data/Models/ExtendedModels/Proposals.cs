using GlassProposalsApp.Data.ViewModels.Proposals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlassProposalsApp.Data.Entities
{
    public partial class Proposals
    {
        public Proposals(Processes process, 
                         Guid initiatorId, 
                         string description = null, 
                         string title = null, 
                         bool isUrgently = false,
                         Guid? vacationId = null)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            InitiatorId = initiatorId;
            Description = description;
            IsClosed = false;
            IsUrgently = isUrgently;
            ProcessId = process.Id;
            Title = title;
            CurrentStageId = process.Stages.First()?.Id;
            VacationId = vacationId;
        }
    }
}
