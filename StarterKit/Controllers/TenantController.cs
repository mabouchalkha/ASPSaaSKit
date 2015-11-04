using StarterKit.Architecture.Bases;
using StarterKit.Architecture.Extensions;
using StarterKit.DOM;
using StarterKit.Helpers;
using StarterKit.Repositories.Interfaces;
using StarterKit.Utils;
using StarterKit.ViewModels;
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
            TenantViewModel currentTenant = _tenantRepository.Read(currentTenantId).MapFromViewModel<TenantViewModel>();

            return success(string.Empty, currentTenant);
        }

        [HttpPut]
        public JsonResult Update(TenantViewModel tenant)
        {
            if (ModelState.IsValid)
            {
                _tenantRepository.Update(tenant.MapFromViewModel<Tenant>());
                return success(MessageUtil.GenerateUpdateSuccessfull(App_GlobalResources.lang.tenant));
            }

            return unsuccess(ErrorUtil.GenerateModelStateError(ModelState));
        }
    }
}