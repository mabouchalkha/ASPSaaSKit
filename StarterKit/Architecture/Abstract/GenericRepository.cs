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
    public abstract class GenericRepository<T, U, TKey> : BaseRepository<T, U, TKey>, IBaseRepository<T, TKey>
        where T : class, IIdentifiableEntity<TKey>, new()
        where U : DbContext, new()
    {    
        protected U GetContext ()
        {
            return new U();
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