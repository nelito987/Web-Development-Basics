using SharpStore.Enums;

namespace SharpStore.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public DeliveryType DeliveryType { get; set; }
    }
}
