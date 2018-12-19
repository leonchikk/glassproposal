using GlassProposalsApp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlassProposalsApp.Data.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private GlassProposalContext _dbContext;

        public ProposalRepository Proposals { get; private set; }
        public UsersRepository Users { get; private set; }

        public UnitOfWork(GlassProposalContext dbContext)
        {
            _dbContext = dbContext;

            if(Proposals == null)
                Proposals = new ProposalRepository(_dbContext);

            if(Users == null)
                Users = new UsersRepository(_dbContext);
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
