﻿using Microsoft.AspNet.Identity;
using StarterKit.Architecture.Interfaces;
using StarterKit.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.Repositories.Interfaces
{
    public interface IGlobalTenantRepository : IBaseRepository<Tenant, Guid>
    {
 
    }
}
