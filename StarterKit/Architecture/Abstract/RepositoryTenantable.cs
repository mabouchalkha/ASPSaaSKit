using StarterKit.DAL;
using StarterKit.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityFramework.DynamicFilters;
using StarterKit.Helpers;
using StarterKit.Architecture.Interfaces;

namespace StarterKit.Architecture.Abstract
{
    // NEVER USE A CONTEXT OUTSIDE OF A REPOSITORY... IF WE DO, TENANTID WILL BE IGNORE AND DATA WILL LEAK
    public abstract class RepositoryTenantable<T>
    {
        protected ApplicationDbContext context = new ApplicationDbContext();

        protected RepositoryTenantable()
        {
            context.EnableFilter("Tenant");
            context.SetFilterScopedParameterValue("Tenant", "currentTenantId", TenantHelper.GetCurrentTenantId());
        }
    }
}