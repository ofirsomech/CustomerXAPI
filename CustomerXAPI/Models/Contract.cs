using CustomerXAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace CustomerXAPI.Models
{
    public class Contract
    {
        [Key]
        public string ID { get; set; }
        [Required]
        public string SubscriptionNumber { get; set; }

        [Required]
        public string SubscriberName { get; set; }

        [Required]
        public eSubscriptionType SubscriptionType { get; set; }

        public virtual ICollection<Package> Packages { get; set; }

        public string CustomerID { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
