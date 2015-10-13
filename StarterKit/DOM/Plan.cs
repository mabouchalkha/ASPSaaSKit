using StarterKit.Architecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.DOM
{
    public class Plan : IIdentifiableEntity<int>
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }

        public ICollection<Feature> Feature { get; set; }

        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
