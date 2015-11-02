using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StarterKit.Mappers
{
    public class Mapper
    {
        public static UviewModel MapToViewModel<Tentity, UviewModel>(Tentity entity)
        {
            return AutoMapper.Mapper.Map<UviewModel>(entity);
        }

        public static Tentity MapFromViewModel<Tentity, UviewModel>(UviewModel viewModel)
        {
            return AutoMapper.Mapper.Map<Tentity>(viewModel);
        }

        public static List<UviewModel> MapToViewModel<Tentity, UviewModel>(List<Tentity> entities)
        {
            List<UviewModel> viewModels = new List<UviewModel>();

            if (entities != null)
            {
                entities.ForEach(e => viewModels.Add(Mapper.MapToViewModel<Tentity, UviewModel>(e)));
            }

            return viewModels;
        }

        public static List<Tentity> MapFromViewModel<Tentity, UviewModel>(List<UviewModel> viewModels)
        {
            List<Tentity> entities = new List<Tentity>();

            if (viewModels != null)
            {
                viewModels.ForEach(v => entities.Add(Mapper.MapFromViewModel<Tentity, UviewModel>(v)));
            }

            return entities;
        }
    }
}
