using StarterKit.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.Architecture.Interfaces
{
    public interface ITenantRepository : IBaseRepository<Tenant, Guid>
    {
    }
}
