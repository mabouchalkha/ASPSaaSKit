using StarterKit.DOM;
using StarterKit.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarterKit.Mappers
{
    public static class TenantMapper
    {
        public static void UpdateUiTenantToDatabase(this Tenant databaseTenant, Tenant uiTenant)
        {
            if (databaseTenant != null)
            {
                if (uiTenant != null)
                {
                    databaseTenant.Name = uiTenant.Name;
                }
            }
        }
    }
}