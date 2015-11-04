using StarterKit.DOM;

namespace StarterKit.Architecture.Bases
{
    public abstract class BaseViewModel<UViewModel>
    {
        public TEntity MapFromViewModel<TEntity>()
        {
            return AutoMapper.Mapper.Map<TEntity>(this);
        }

        public static UViewModel MapToViewModel<TEntity>(TEntity entity)
        {
            return AutoMapper.Mapper.Map<UViewModel>(entity);
        }
    }
}

