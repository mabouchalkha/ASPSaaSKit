using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.Architecture.Interfaces.Mapping
{
    public interface IMapFromCustom
    {
        void CreateMappings(IConfiguration configuration);
    }
}
