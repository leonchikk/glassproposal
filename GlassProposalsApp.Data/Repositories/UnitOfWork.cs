using GlassProposalsApp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlassProposalsApp.Data.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private GlassProposalContext _dbContext;

        public IProposalRepository Proposals { get; }
        public IUsersRepository Users { get; }

        public UnitOfWork(GlassProposalContext dbContext, IProposalRepository proposalRepository, IUsersRepository usersRepository)
        {
            _dbContext = dbContext;
            Proposals = proposalRepository;
            Users = usersRepository;
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
