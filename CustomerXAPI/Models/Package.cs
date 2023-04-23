using CustomerXAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace CustomerXAPI.Models
{
    public class Package
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public ePackageType PackageType { get; set; }

        [Required]
        public string PackageName { get; set; }

        [Required]
        public int Amount { get; set; }

        public int Survivor { get; set; }

        public int ContractSubscriptionNumber { get; set; }

        public virtual Contract Contract { get; set; }
    }
}
