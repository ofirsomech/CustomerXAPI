using CustomerXAPI.Enums;

namespace CustomerXAPI.Dtos
{
    public class PackageCreateDto
    {
        public ePackageType PackageType { get; set; }
        public string PackageName { get; set; }
        public int Amount { get; set; }
        public int Survivor { get; set; }
        public int ContractSubscriptionNumber { get; set; }
    }

    public class PackageReadDto
    {
        public int ID { get; set; }
        public ePackageType PackageType { get; set; }
        public string PackageName { get; set; }
        public int Amount { get; set; }
        public int Survivor { get; set; }
        public int ContractSubscriptionNumber { get; set; }
    }

    public class PackageUpdateDto
    {
        public ePackageType PackageType { get; set; }
        public string PackageName { get; set; }
        public int Amount { get; set; }
        public int Survivor { get; set; }
        public int ContractSubscriptionNumber { get; set; }
    }
}
