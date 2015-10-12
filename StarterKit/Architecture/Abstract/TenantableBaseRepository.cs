using EntityFramework.DynamicFilters;
using StarterKit.Architecture.Interfaces;
using StarterKit.DAL;
using StarterKit.Helpers;
using System;

namespace StarterKit.Architecture.Abstract
{
    public abstract class TenantableBaseRepository<T, TKey> : BaseRepository<T, ApplicationDbContext, TKey>
         where T : class, IIdentifiableEntity<TKey>, new()
    {
    }
}