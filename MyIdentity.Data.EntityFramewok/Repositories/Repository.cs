using MyIdentity.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Entity;

namespace MyIdentity.Data.EntityFramewok.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private ApplicationDbContext _context;
        private DbSet<TEntity> _set;

        public Repository(ApplicationDbContext context)
        {
            _context = context;            
        }

        protected DbSet<TEntity> Set
        {
            get { return _set ?? (_set = _context.Set<TEntity>()); }
        }
        public void Add(TEntity entity)
        {
            Set.Add(entity);
        }

        public TEntity FindById(object id)
        {
            return Set.Find(id);
        }

        public Task<TEntity> FindByIdAsync(object id)
        {
            return Set.FindAsync(id);
        }

        public Task<TEntity> FindByIdAsync(CancellationToken cancellationToken, object id)
        {
            return Set.FindAsync(cancellationToken, id);
        }

        public List<TEntity> GetAll()
        {
            return Set.ToList();
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return Set.ToListAsync();
        }

        public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return Set.ToListAsync(cancellationToken);
        }

        public List<TEntity> PageAll(int skip, int take)
        {
            return Set.Skip(skip).Take(take).ToList();
        }

        public Task<List<TEntity>> PageAllAsync(int skip, int take)
        {
            return Set.Skip(skip).Take(take).ToListAsync();
        }

        public Task<List<TEntity>> PageAllAsync(CancellationToken cancellationtoken, int skip, int take)
        {
            return Set.Skip(skip).Take(take).ToListAsync(cancellationtoken);
        }

        public void Remove(TEntity entity)
        {
            Set.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                Set.Attach(entity);
                entry = _context.Entry(entity);
            }
            entry.State = EntityState.Modified;
        }
    }
}
