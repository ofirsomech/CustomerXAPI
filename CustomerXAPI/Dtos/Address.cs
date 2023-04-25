using System.ComponentModel.DataAnnotations;

namespace CustomerXAPI.Dtos
{
    public class UpdateAddressDto
    {
        public string AddressCity { get; set; }
        public string AddressStreet { get; set; }
        public string AddressHouseNumber { get; set; }
        public string PostalCode { get; set; }
    }
}