using StarterKit.Architecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarterKit.Architecture.Extensions
{
    public static class MapperExtension
    {
         public static Tentity MapToViewModel<Tentity>(this ICanMap viewModel)
         {
            return AutoMapper.Mapper.Map<Tentity>(viewModel);
         }

        public static UviewModel MapFromViewModel<UviewModel>(this ICanMap entity)
        {
            return AutoMapper.Mapper.Map<UviewModel>(entity);
        }

        public static IEnumerable<Tentity> MapToViewModel<Tentity>(this IEnumerable<ICanMap> viewModel)
        {
            return AutoMapper.Mapper.Map<IEnumerable<Tentity>>(viewModel);
        }

        public static IEnumerable<UviewModel> MapFromViewModel<UviewModel>(this IEnumerable<ICanMap> entity)
        {
            return AutoMapper.Mapper.Map<IEnumerable<UviewModel>>(entity);
        }
    }
}