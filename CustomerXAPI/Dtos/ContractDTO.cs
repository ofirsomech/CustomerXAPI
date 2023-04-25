using CustomerXAPI.Enums;
using CustomerXAPI.Models;

namespace CustomerXAPI.Dtos
{
    public class ContractCreateDto
    {
        public int CustomerID { get; set; }
        public string SubscriptionNumber { get; set; }
        public string SubscriberName { get; set; }
        public eSubscriptionType SubscriptionType { get; set; }
        public ICollection<int> PackageIds { get; set; }
    }

    public class ContractReadDto
    {
        public string ID { get; set; }
        public int CustomerID { get; set; }
        public string SubscriptionNumber { get; set; }
        public string SubscriberName { get; set; }
        public eSubscriptionType SubscriptionType { get; set; }
        public ICollection<PackageReadDto> Packages { get; set; }
    }

    public class ContractUpdateDto
    {
        public int CustomerID { get; set; }
        public string SubscriptionNumber { get; set; }
        public string SubscriberName { get; set; }
        public eSubscriptionType SubscriptionType { get; set; }
        public ICollection<int> PackageIds { get; set; }
    }
}
