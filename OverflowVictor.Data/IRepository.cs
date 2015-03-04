using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using OverflowVictor.Domain.Entities;

namespace OverflowVictor.Data
{
    public interface IRepository<TEntity> : IDisposable
    {
        IEnumerable<TEntity> GetEntities();
        TEntity GetEntityById(Guid entityId);
        void InsertEntity(TEntity entity);
        void DeleteEntity(Guid entityId);
        void UpdateEntity(TEntity entity);
        void Save();
    }
}