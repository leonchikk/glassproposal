using Novacode;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlassProposalsApp.Domain.Interfaces
{
    public interface IExportService
    {
        DocX ExportVacationProposalAsDocx(Guid proposalId);
    }
}
