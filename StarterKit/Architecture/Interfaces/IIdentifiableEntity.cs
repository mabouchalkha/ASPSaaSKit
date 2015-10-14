using StarterKit.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarterKit.Architecture.Interfaces
{
    public interface IIdentifiableEntity
    {
   
    }

    public interface IIdentifiableEntity<TKey> : IIdentifiableEntity
    {
        TKey EntityId { get; set; }
    }
}