using StarterKit.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarterKit.Architecture.Interfaces
{
    public interface ITenantable
    {
        Guid TenantId { get; set; }
        Tenant Tenant { get; set; }
    }
}