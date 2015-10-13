using System;
using StarterKit.Architecture.Interfaces;

namespace StarterKit.DOM
{
    public class Feature : IIdentifiableEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }

        public Plan Plan { get; set; }

        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}