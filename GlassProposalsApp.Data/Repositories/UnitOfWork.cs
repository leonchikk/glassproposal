using GlassProposalsApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlassProposalsApp.Data.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private GlassProposalContext db;
        private IProposalRepository _proposalRepository;

        public UnitOfWork(GlassProposalContext db, IProposalRepository _proposalRepository)
        {

        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
