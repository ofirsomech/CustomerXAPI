using CustomerXAPI.Enums;

namespace CustomerXAPI.Dtos
{
    public class ContractCreateDto
    {
        public string SubscriberName { get; set; }
        public eSubscriptionType SubscriptionType { get; set; }
        public ICollection<int> PackageIds { get; set; }
        public string CustomerID { get; set; }
    }

    public class ContractReadDto
    {
        public string SubscriptionNumber { get; set; }
        public string SubscriberName { get; set; }
        public eSubscriptionType SubscriptionType { get; set; }
        //public ICollection<PackageReadDto> Packages { get; set; }
        public string CustomerID { get; set; }
    }

    public class ContractUpdateDto
    {
        public string SubscriberName { get; set; }
        public eSubscriptionType SubscriptionType { get; set; }
        public ICollection<int> PackageIds { get; set; }
        public string CustomerID { get; set; }
    }
}
