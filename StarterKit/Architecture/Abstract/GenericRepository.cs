using StarterKit.Architecture.Interfaces;
using StarterKit.Extentions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;

namespace StarterKit.Architecture.Abstract
{
    public abstract class GenericRepository<T, U, TKey> : BaseRepository<T, U, TKey>, IBaseRepository<T, TKey>
        where T : class, IIdentifiableEntity<TKey>, new()
        where U : DbContext, new()
    {
        protected U GetContext()
        {
            U context = new U();
            context.Database.Log = Console.Write;

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

        public virtual void DeleteBy(Expression<Func<T, bool>> predicate)
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

        public T FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
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
                return base.UpdateGeneric(entityContext, entity);
            }
        }
    }
}