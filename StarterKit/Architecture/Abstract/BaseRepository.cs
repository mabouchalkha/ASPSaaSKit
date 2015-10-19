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

        private T GetEntity(U entityContext, TKey id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbSet(entityContext);
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.FirstOrDefault(IdentifierPredicate(entityContext, id));
        }

        private T UpdateEntity(U entityContext, T entity)
        {
            return DbSet(entityContext).FirstOrDefault(IdentifierPredicate(entityContext, entity.EntityId));
        }

        private T AddEntity(U entityContext, T entity)
        {
            return DbSet(entityContext).Add(entity);
        }

        private IEnumerable<T> GetEntities(U entityContext, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbSet(entityContext);
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        protected T CreateGeneric(U entityContext, T entity)
        {
            T addedEntity = AddEntity(entityContext, entity);
            entityContext.SaveChanges();

            return addedEntity;
        }

        protected void DeleteGeneric(U entityContext, T entity)
        {
            entityContext.Entry<T>(entity).State = EntityState.Deleted;
            entityContext.SaveChanges();
        }

        protected void DeleteGeneric(U entityContext, TKey id)
        {
            T entity = GetEntity(entityContext, id);
            entityContext.Entry<T>(entity).State = EntityState.Deleted;
            entityContext.SaveChanges();
        }

        protected void DeleteByGeneric(U entityContext, Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = DbSet(entityContext);
            T entity = query.FirstOrDefault(predicate);
            entityContext.Entry<T>(entity).State = EntityState.Deleted;
            entityContext.SaveChanges();
        }

        protected IEnumerable<T> IndexGeneric(U entityContext, params Expression<Func<T, object>>[] includeProperties)
        {
            return GetEntities(entityContext, includeProperties);
        }

        protected T ReadGeneric(U entityContext, TKey id, params Expression<Func<T, object>>[] includeProperties)
        {
            return GetEntity(entityContext, id, includeProperties);
        }

        protected T FindByGeneric(U entityContext, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbSet(entityContext);
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.FirstOrDefault(predicate);
        }

        protected T UpdateGeneric(U entityContext, T entity)
        {
            entityContext.Entry(entity).State = EntityState.Modified;
            entityContext.SaveChanges();

            return entity;  
        }
    }
}