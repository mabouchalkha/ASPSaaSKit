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
        protected abstract void ValidateTenant(T entity);

        protected U GetContext()
        {
            U context = new U();
            context.EnableFilter("Tenant");
            context.SetFilterScopedParameterValue("Tenant", "currentTenantId", TenantHelper.GetCurrentTenantId());
            context.Database.Log = Console.Write;

            return context;
        }

        public virtual T Create(T entity)
        {
            using (U entityContext = this.GetContext())
            {
                this.ValidateTenant(entity);
                return base.CreateGeneric(entityContext, entity);
            }
        }

        public virtual void Delete(T entity)
        {
            using (U entityContext = this.GetContext())
            {
                this.ValidateTenant(entity);
                base.DeleteGeneric(entityContext, entity);
            }
        }

        public virtual void Delete(TKey id)
        {
            using (U entityContext = this.GetContext())
            {
                this.ValidateTenant(this.Read(id));
                base.DeleteGeneric(entityContext, id);
            }
        }

        public void DeleteBy(Expression<Func<T, bool>> predicate)
        {
            using (U entityContext = this.GetContext())
            {
                base.DeleteByGeneric(entityContext, predicate);
            }
        }

        public virtual IEnumerable<T> Index(params Expression<Func<T, object>>[] includeProperties)
        {
            using (U entityContext = this.GetContext())
            {
                return base.IndexGeneric(entityContext, includeProperties).ToFullyLoaded();
            }
        }

        public virtual T Read(TKey id, params Expression<Func<T, object>>[] includeProperties)
        {
            using (U entityContext = this.GetContext())
            {
                return base.ReadGeneric(entityContext, id, includeProperties);
            }
        }

        public virtual T FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            using (U entityContext = this.GetContext())
            {
                return base.FindByGeneric(entityContext, predicate, includeProperties);
            }
        }

        public virtual T Update(T entity)
        {
            using (U entityContext = this.GetContext())
            {
                this.ValidateTenant(entity);
                return base.UpdateGeneric(entityContext, entity);
            }
        }
    }
}