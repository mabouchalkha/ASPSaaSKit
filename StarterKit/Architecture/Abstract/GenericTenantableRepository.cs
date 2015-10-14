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
    public abstract class GenericTenantableRepository<T, U, TKey> : BaseRepository<T, U, TKey>, IBaseRepository<T, TKey>
        where T : class, IIdentifiableEntity<TKey>, new()
        where U : DbContext, new()
    {
        protected U GetContext()
        {
            U context = new U();
            context.EnableFilter("Tenant");
            context.SetFilterScopedParameterValue("Tenant", "currentTenantId", TenantHelper.GetCurrentTenantId());

            return context;
        }

        public virtual T Create(T entity)
        {
            using (U entityContext = this.GetContext())
            {
                return base.CreateGeneric(entityContext, entity);
            }
        }

        public virtual void Delete(T entity)
        {
            using (U entityContext = this.GetContext())
            {
                base.DeleteGeneric(entityContext, entity);
            }
        }

        public virtual void Delete(TKey id)
        {
            using (U entityContext = this.GetContext())
            {
                base.DeleteGeneric(entityContext, id);
            }
        }

        public virtual IEnumerable<T> Index()
        {
            using (U entityContext = this.GetContext())
            {
                return base.IndexGeneric(entityContext);
            }
        }

        public virtual T Read(TKey id)
        {
            using (U entityContext = this.GetContext())
            {
                return base.ReadGeneric(entityContext, id);
            }
        }

        public virtual T Update(T entity)
        {
            using (U entityContext = this.GetContext())
            {
                return base.UpdateGeneric(entityContext, entity);
            }
        }
    }
}