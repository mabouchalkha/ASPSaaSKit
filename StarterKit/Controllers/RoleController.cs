using StarterKit.Architecture.Bases;
using StarterKit.Architecture.Extensions;
using StarterKit.Repositories.Interfaces;
using StarterKit.ViewModels;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Mvc;

namespace StarterKit.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize]
    public class RoleController : BaseController
    {
        private IRoleRepository _roleRepository;

        [ImportingConstructor]
        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public JsonResult Index()
        {
            return success(string.Empty, new { entities = _roleRepository.Index().ToList().MapToViewModel<RoleViewModel>() });
        }
    }
}