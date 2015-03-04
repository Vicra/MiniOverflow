using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OverflowVictor.Data
{
    public class Repository<TEntity> : IRepository<TEntity>,IDisposable where TEntity : class
    {
        private OverflowVictorContext context;
        private DbSet<TEntity> DbSet;
        private bool disposed;
        

        public Repository(OverflowVictorContext context)
        {
            this.context = context;
            this.DbSet = context.Set<TEntity>();
            disposed = false;
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<TEntity> GetEntities()
        {
            return DbSet.ToList();
        }

        private bool Compare(TEntity x, TEntity y)
        {
            return EqualityComparer<TEntity>.Default.Equals(x,y);
        }

        public TEntity GetEntityById(Guid entityId)
        {
            return DbSet.Find(entityId);
        }

        public void InsertEntity(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void DeleteEntity(Guid entityId)
        {
            TEntity entity = DbSet.Find(entityId);
            DbSet.Remove(entity);
        }

        public void UpdateEntity(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}