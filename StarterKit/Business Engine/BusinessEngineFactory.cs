using StarterKit.Business_Engine.Interfaces;
using System.ComponentModel.Composition;
using System.Web.Mvc;

namespace StarterKit.Business_Engine
{
    [Export(typeof(IBusinessEngineFactory))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BusinessEngineFactory : IBusinessEngineFactory
    {
        T IBusinessEngineFactory.GetBusinessEngine<T>()
        {
            return DependencyResolver.Current.GetService<T>();
        }
    }
}
