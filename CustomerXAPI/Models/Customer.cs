using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace CustomerXAPI.Models
{

    public class Customer
    {

        [Key]
        public int ID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string IdentityNumber { get; set; }


        public string AddressCity { get; set; }

        public string AddressStreet { get; set; }

        public string AddressHouseNumber { get; set; }

        public string PostalCode { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
