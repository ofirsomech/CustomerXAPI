using CustomerXAPI.Enums;
using CustomerXAPI.Models;

namespace CustomerXAPI.Dtos
{
    public class PackageCreateDto
    {
        public ePackageType PackageType { get; set; }
        public int ContractID { get; set; }
        public string PackageName { get; set; }
        public int Amount { get; set; }
        public int Used { get; set; }
    }

    public class PackageReadDto
    {
        public int ID { get; set; }
        public int ContractID { get; set; }
        public ePackageType PackageType { get; set; }
        public string PackageName { get; set; }
        public int Amount { get; set; }
        public int Used { get; set; }
    }

    public class PackageUpdateDto
    {
        public ePackageType PackageType { get; set; }
        public int ContractID { get; set; }
        public string PackageName { get; set; }
        public int Amount { get; set; }
        public int Used { get; set; }
    }

}
