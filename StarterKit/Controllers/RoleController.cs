using StarterKit.Architecture.Bases;
using StarterKit.Architecture.Interfaces;
using StarterKit.DOM;
using StarterKit.Helpers;
using StarterKit.Mappers;
using StarterKit.Repositories;
using StarterKit.Repositories.Interfaces;
using StarterKit.Utils;
using StarterKit.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
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
            return success(string.Empty, new { entities = _roleRepository.Index().ToList().MapToViewModel() });
        }
    }
}