using GlassProposalsApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlassProposalsApp.Data.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private GlassProposalContext _dbContext;

        public IProposalRepository Proposals { get; }

        public UnitOfWork(GlassProposalContext dbContext, IProposalRepository proposalRepository)
        {
            _dbContext = dbContext;
            Proposals = proposalRepository;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
