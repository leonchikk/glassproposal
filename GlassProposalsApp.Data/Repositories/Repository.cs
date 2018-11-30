using GlassProposalsApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlassProposalsApp.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly GlassProposalContext Db;
        protected readonly DbSet<T> DbSet;

        public Repository(GlassProposalContext context)
        {
            Db = context;
            DbSet = Db.Set<T>();
        }

        public void Add(T obj)
        {
            DbSet.Add(obj);
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public void Remove(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public void Update(T obj)
        {
            DbSet.Update(obj);
        }
    }
}
