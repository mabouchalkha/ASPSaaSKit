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
    // NEVER USE A CONTEXT OUTSIDE OF A REPOSITORY... IF WE DO, TENANTID WILL BE IGNORE AND DATA WILL LEAK
    public class RepositoryTenantable : IRepositoryTenantable
    {
        protected ApplicationDbContext context;

        protected RepositoryTenantable(ApplicationDbContext _context)
        {
            context = _context;
            context.EnableFilter("Tenant");
            context.SetFilterScopedParameterValue("Tenant", "currentTenantId", TenantHelper.GetCurrentTenantId());
        }
    }
}