using StarterKit.Architecture.Bases;
using StarterKit.Architecture.Interfaces;
using StarterKit.DOM;
using StarterKit.Helpers;
using StarterKit.Mappers;
using StarterKit.Repositories;
using StarterKit.Repositories.Interfaces;
using StarterKit.Utils;
using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;

namespace StarterKit.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize(Roles = "Owner, Admin")]
    public class TenantController : BaseController
    {
        private ITenantRepository _tenantRepository;

        [ImportingConstructor]
        public TenantController(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        [HttpGet]
        public JsonResult Read()
        {
            Guid currentTenantId = TenantHelper.GetCurrentTenantId();
            Tenant currentTenant = _tenantRepository.Read(currentTenantId);

            return success(string.Empty, currentTenant);
        }

        [HttpPut]
        public JsonResult Update(Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                Guid currentTenantId = TenantHelper.GetCurrentTenantId();

                if (currentTenantId == tenant.Id)
                {
                    Tenant databaseTenant = _tenantRepository.Read(currentTenantId);

                    if (databaseTenant != null)
                    {
                        databaseTenant.UpdateUiTenantToDatabase(tenant);
                        databaseTenant = _tenantRepository.Update(databaseTenant);

                        return success("Account successfully updated");
                    }
                    else
                    {
                        return unsuccess(string.Format("Tenant with ID {0} cannot be found in database", tenant.Id));
                    }
                }
                else
                {
                    return unsuccess("You cannot edit an account that you are part of");
                }
                
            }

            return unsuccess(ErrorUtil.GenerateModelStateError(ModelState));
        }
    }
}