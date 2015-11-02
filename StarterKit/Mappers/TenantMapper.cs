using StarterKit.DOM;
using StarterKit.Repositories.Interfaces;
using StarterKit.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarterKit.Mappers
{
    public static class TenantMapper
    {
        public static TenantViewModel MapToViewModel(this Tenant tenant)
        {
            return new TenantViewModel()
            {
                Id = tenant.TenantId,
                IsTrial = tenant.IsTrial,
                Name = tenant.Name
            };
        }

        public static Tenant MapFromViewModel(this TenantViewModel tenant, Tenant databaseTenant)
        {
            databaseTenant.Name = tenant.Name;

            return databaseTenant;
        }
    }
}