using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlassProposalsApp.Data.Interfaces
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        void Add(T obj);
        IQueryable<T> GetAll();
        void Update(T obj);
        void Remove(Guid id);
    }
}
