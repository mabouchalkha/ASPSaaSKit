﻿using StarterKit.Architecture.Abstract;
using StarterKit.DAL;
using StarterKit.DOM;
using StarterKit.Repositories.Interfaces;
using System;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq.Expressions;

namespace StarterKit.Repositories
{
    [Export(typeof(IGlobalTenantRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GlobalTenantRepository : GenericRepository<Tenant, ApplicationDbContext, Guid>, IGlobalTenantRepository
    {
        protected override DbSet<Tenant> DbSet(ApplicationDbContext entityContext)
        {
            return entityContext.Tenants;
        }

        protected override Expression<Func<Tenant, bool>> IdentifierPredicate(ApplicationDbContext entityContext, Guid id)
        {
            return (e => e.TenantId == id);
        }
    }
}