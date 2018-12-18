using GlassProposalsApp.Data.Entities;
using GlassProposalsApp.Data.Enumerations;
using GlassProposalsApp.Data.Repositories;
using GlassProposalsApp.Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Novacode;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace GlassProposalsApp.Domain.Services
{
    public class ExportService: IExportService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UnitOfWork _unitOfWork;

        public ExportService(IHostingEnvironment hostingEnvironment, UnitOfWork unitOfWork)
        {
            _hostingEnvironment = hostingEnvironment;
            _unitOfWork = unitOfWork;
        }

        public DocX ExportVacationProposalAsDocx(Guid proposalId)
        {
            var proposal = _unitOfWork.Proposals.GetById(proposalId);

            var templateFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "VacationLetterTemplate.docx");
            var docFile = DocX.Load(templateFilePath);

            #region Replace text
            docFile.ReplaceText("{INITIATOR_NAME}", proposal.Initiator.Name);
            docFile.ReplaceText("{CREATED_AT}", proposal.CreatedAt.ToString("dd/MM/yyyy"));
            docFile.ReplaceText("{START_DATE}", proposal.Vacation.StartDate.ToString("dd/MM/yyyy"));
            docFile.ReplaceText("{END_DATE}", proposal.Vacation.EndDate.ToString("dd/MM/yyyy"));
            docFile.ReplaceText("{DAYS_COUNT}", (proposal.Vacation.EndDate - proposal.Vacation.StartDate).Days.ToString());
            #endregion

            var statuses = proposal.Statuses.OrderBy(status => status.UpdatedAt).ToList();

            #region Set border style (it's shit)
            Border borderStyle = new Border(BorderStyle.Tcbs_double, BorderSize.one, 0, Color.White);

            Table approveTable = docFile.AddTable(statuses.Count, 2);
            approveTable.SetBorder(TableBorderType.InsideH, borderStyle);
            approveTable.SetBorder(TableBorderType.InsideV, borderStyle);
            approveTable.SetBorder(TableBorderType.Bottom, borderStyle);
            approveTable.SetBorder(TableBorderType.Top, borderStyle);
            approveTable.SetBorder(TableBorderType.Left, borderStyle);
            approveTable.SetBorder(TableBorderType.Right, borderStyle);
            #endregion

            for (int i = 0; i < statuses.Count; i++)
            {
                approveTable.Rows[i].Cells[0].Paragraphs.First().Append(
                      $"DATE: {statuses[i].UpdatedAt?.ToString("dd/MM/yyyy")}\n" +
                      $"Approved {GetDecisionMakerPosition(statuses[i].DecisionMaker)}\n")
                    ;

                approveTable.Rows[i].Cells[1].Paragraphs.First().Append($"\nBy: {statuses[i].DecisionMaker.Name}\n");
            }

            docFile.InsertTable(approveTable);
            docFile.SaveAs(Path.Combine(_hostingEnvironment.WebRootPath, $"{proposal.Initiator.Name} vacation.docx"));

            return docFile;
        }

        private string GetDecisionMakerPosition(Users decisionMaker)
        {
            var decisionType = decisionMaker.UserTypes.FirstOrDefault(x => x.UserType != (int)UsersTypes.Developer);

            switch (decisionType.UserType)
            {
                case (int)UsersTypes.HR: return "HR manager";
                case (int)UsersTypes.PM: return "Project manager";
                case (int)UsersTypes.TeamLead: return "Team leader";
                case (int)UsersTypes.Director: return "Director";
            }
            return null;
        }
    }
}
