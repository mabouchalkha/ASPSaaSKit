using EntityFramework.DynamicFilters;
using StarterKit.Architecture.Interfaces;
using StarterKit.DAL;
using StarterKit.Extentions;
using StarterKit.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace StarterKit.Architecture.Abstract
{
    public abstract class GenericTenantableRepository<T, U, TKey> : IBaseRepository<T, TKey>
        where T : class, IIdentifiableEntity<TKey>, new()
        where U : DbContext, new()
    {
        protected abstract DbSet<T> DbSet(U entityContext);
        protected abstract Expression<Func<T, bool>> IdentifierPredicate(U entityContext, TKey id);

        protected U GetContext()
        {
            U context = new U();
            context.EnableFilter("Tenant");
            context.SetFilterScopedParameterValue("Tenant", "currentTenantId", TenantHelper.GetCurrentTenantId());

            return context;
        }

        T AddEntity(U entityContext, T entity)
        {
            return DbSet(entityContext).Add(entity);
        }

        IEnumerable<T> GetEntities(U entityContext)
        {
            return DbSet(entityContext).ToFullyLoaded();
        }

        T GetEntity(U entityContext, TKey id)
        {
            return DbSet(entityContext).Where(IdentifierPredicate(entityContext, id)).FirstOrDefault();
        }

        T UpdateEntity(U entityContext, T entity)
        {
            var q = DbSet(entityContext).Where(IdentifierPredicate(entityContext, entity.EntityId));
            return q.FirstOrDefault();
        }

        public virtual T Create(T entity)
        {
            using (U entityContext = this.GetContext())
            {
                T addedEntity = AddEntity(entityContext, entity);
                entityContext.SaveChanges();
                return addedEntity;
            }
        }

        public virtual void Delete(T entity)
        {
            using (U entityContext = new U())
            {
                entityContext.Entry<T>(entity).State = EntityState.Deleted;
                entityContext.SaveChanges();
            }
        }

        public virtual void Delete(TKey id)
        {
            using (U entityContext = this.GetContext())
            {
                T entity = GetEntity(entityContext, id);
                entityContext.Entry<T>(entity).State = EntityState.Deleted;
                entityContext.SaveChanges();
            }
        }

        public virtual IEnumerable<T> Index()
        {
            using (U entityContext = new U())
            {
                return (GetEntities(entityContext)).ToArray().ToList();
            }
        }

        public virtual T Read(TKey id)
        {
            using (U entityContext = this.GetContext())
            {
                return GetEntity(entityContext, id);
            }
        }

        public virtual T Update(T entity)
        {
            using (U entityContext = new U())
            {
                T existingEntity = UpdateEntity(entityContext, entity);

                // maybe a automapper
                ////SimpleMapper.PropertyMap(entity, existingEntity);

                entityContext.SaveChanges();
                return existingEntity;
            }
        }
    }
}