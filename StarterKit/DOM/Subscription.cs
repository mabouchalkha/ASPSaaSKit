using StarterKit.Architecture.Interfaces;

namespace StarterKit.DOM
{
    public class Subscription : IIdentifiableEntity<int>
    {
        public int Id { get; set; }
        public string StripeChargeId { get; set; }

        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
