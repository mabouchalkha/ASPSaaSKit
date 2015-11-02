using StarterKit.Architecture.Interfaces;
using System;

namespace StarterKit.DOM
{
    public class StripeEventLog : IIdentifiableEntity<int>
    {
        public int Id { get; set; }
        public string stripeEventId { get; set; }
        public string Request { get; set; }
        public string Type { get; set; }
        public string UserId { get; set; }
        public bool LiveMode { get; set; }
        public DateTime EventDate { get; set; }

        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
