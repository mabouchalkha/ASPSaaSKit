using StarterKit.Repositories;
using System.Web.Mvc;
using StarterKit.Mappers;
using System;
using StarterKit.ViewModels;
using StarterKit.DOM;
using System.Threading.Tasks;
using StarterKit.Utils;
using StarterKit.Helpers;
using Microsoft.AspNet.Identity;
using System.Linq;

namespace StarterKit.Controllers
{
    [Authorize(Roles = "Owner, Admin")]
    public class TenantController : BaseController
    {
        private TenantRepository _tenantRepo = new TenantRepository();

        [HttpGet]
        public JsonResult Read()
        {
            Guid currentTenantId = TenantHelper.GetCurrentTenantId();
            Tenant currentTenant = _tenantRepo.Read(currentTenantId);

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
                    Tenant databaseTenant = _tenantRepo.Read(currentTenantId);

                    if (databaseTenant != null)
                    {
                        databaseTenant.UpdateUiTenantToDatabase(tenant);
                        bool isUpdated = _tenantRepo.Update(databaseTenant);

                        if (isUpdated)
                        {
                            return success("Account successfully updated");
                        }
                        else
                        {
                            return unsuccess("Cannot save this account. Please refresh and try again");
                        }
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

            return unsuccess(ErrorUtil.DefaultError);
        }
    }
}