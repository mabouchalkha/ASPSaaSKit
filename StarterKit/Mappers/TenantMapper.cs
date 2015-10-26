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
                Id = tenant.Id,
                IsTrial = tenant.IsTrial,
                Name = tenant.Name
            };
        }

        public static Tenant MapFromViewModel(this TenantViewModel tenant)
        {
            return new Tenant()
            {
                Id = tenant.Id,
                Name = tenant.Name
            };
            //Tenant databaseTenant = _repo.Read(tenant.Id);

            //if (databaseTenant != null)
            //{
            //    databaseTenant.Name = tenant.Name;
            //}
            //else
            //{
            //    throw new InvalidOperationException(string.Format(App_GlobalResources.lang.entityNotFound, tenant.Id));
            //}

            //return databaseTenant;
        }
    }
}