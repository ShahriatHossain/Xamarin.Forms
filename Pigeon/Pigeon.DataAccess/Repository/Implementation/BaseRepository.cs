using Pigeon.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Pigeon.DataAccess.Repository.Implementation
{
    public abstract class BaseRepository
    {
        protected void SafeCall(Action action)
        {
            action();
        }

        protected TResult SafeCall<TResult>(Func<TResult> action)
        {
            return action();
        }

    }

    public abstract class BaseRepository<TEntity>: BaseRepository
        where TEntity : BaseModel
    {
        private PigeonDbContext _context;

        protected BaseRepository(PigeonDbContext context)
        {
            _context = context;            
        }

        protected PigeonDbContext Context => this._context;

        private DbSet<TEntity> Entity => this.Context.Set<TEntity>();
               

        public virtual TEntity GetById(int id)
        {
            return SafeCall(() => this.Entity.FirstOrDefault(x => x.Id == id));
        }

        public virtual IEnumerable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate)
        {            
            return SafeCall(() => this.Entity.Where(predicate));
        }

        public TEntity Save(TEntity entity)
        {
            return SafeCall(() =>
            {
                if (entity.Id != 0)
                {
                    this.Context.Update(entity);
                }
                else
                {
                    this.Context.Add(entity);
                }
                this.Context.SaveChanges();
                return entity;
            });
        }

        public virtual IEnumerable<TEntity> Get()
        {
            return SafeCall(() => this.Entity);
        }

        protected IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return SafeCall(() => this.Entity.Where(predicate));
        }

        public void Delete(TEntity item)
        {
            SafeCall(() =>
            {
                this.Context.Remove(item);
                this.Context.SaveChanges();
            });
        }

    }
}
