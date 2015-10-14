using EntityFramework.DynamicFilters;
using StarterKit.Architecture.Interfaces;
using StarterKit.DAL;
using StarterKit.Helpers;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using StarterKit.Extentions;
using StarterKit.Mappers;

namespace StarterKit.Architecture.Abstract
{
    public abstract class BaseRepository<T, U, TKey>
        where T : class, IIdentifiableEntity<TKey>, new()
        where U : DbContext, new()
    {
        protected abstract Expression<Func<T, bool>> IdentifierPredicate(U entityContext, TKey id);
        protected abstract DbSet<T> DbSet(U entityContext);

        private T GetEntity(U entityContext, TKey id)
        {
            return DbSet(entityContext).Where(IdentifierPredicate(entityContext, id)).FirstOrDefault();
        }

        private T UpdateEntity(U entityContext, T entity)
        {
            var q = DbSet(entityContext).Where(IdentifierPredicate(entityContext, entity.EntityId));
            return q.FirstOrDefault();
        }

        private T AddEntity(U entityContext, T entity)
        {
            return DbSet(entityContext).Add(entity);
        }

        private IEnumerable<T> GetEntities(U entityContext)
        {
            return DbSet(entityContext).ToFullyLoaded();
        }

        public T CreateGeneric(U entityContext, T entity)
        {
            T addedEntity = AddEntity(entityContext, entity);
            entityContext.SaveChanges();

            return addedEntity;
        }

        public void DeleteGeneric(U entityContext, T entity)
        {
            entityContext.Entry<T>(entity).State = EntityState.Deleted;
            entityContext.SaveChanges();
        }

        public void DeleteGeneric(U entityContext, TKey id)
        {
            T entity = GetEntity(entityContext, id);
            entityContext.Entry<T>(entity).State = EntityState.Deleted;
            entityContext.SaveChanges();
        }

        public IEnumerable<T> IndexGeneric(U entityContext)
        {
            return (GetEntities(entityContext)).ToArray().ToList();
        }

        public T ReadGeneric(U entityContext, TKey id)
        {
            return GetEntity(entityContext, id);
        }

        public T UpdateGeneric(U entityContext, T entity)
        {
            entityContext.Entry(entity).State = EntityState.Modified;
            entityContext.SaveChanges();

            return entity;  
        }
    }
}