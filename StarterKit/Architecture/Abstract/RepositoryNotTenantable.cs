using StarterKit.DAL;
using StarterKit.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityFramework.DynamicFilters;
using StarterKit.Helpers;
using StarterKit.Architecture.Interfaces;
using LegalIt.Architecture.Interfaces;

namespace StarterKit.Architecture.Abstract
{
    public abstract class RepositoryNotTenantable : IRepositoryNotTenantable
    {
        protected ApplicationDbContext context;

        protected RepositoryNotTenantable(ApplicationDbContext context)
        {
            this.context = context;
            //context.DisableFilter("Tenant");
        }
    }
}