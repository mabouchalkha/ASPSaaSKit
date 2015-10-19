using StarterKit.Repositories.Interfaces;
using System.ComponentModel.Composition;
using System.Web.Mvc;

namespace StarterKit.Repositories
{
    [Export(typeof(IDataRepositoryFactory))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DataRepositoryFactory : IDataRepositoryFactory
    {
        T IDataRepositoryFactory.GetDataRepository<T>()
        {
            return DependencyResolver.Current.GetService<T>();
        }
    }
}
