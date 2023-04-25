using CustomerXAPI.Dtos;

namespace CustomerXAPI.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerReadDto>> GetAllCustomersAsync();
        Task<CustomerReadDto> GetCustomerByIdentityNumberAsync(string identityNumber);
        Task<CustomerReadDto> GetCustomerByIdAsync(int id);
        Task<CustomerReadDto> UpdateCustomerAddressAsync(int id, UpdateAddressDto updateAddressDto);
        Task<CustomerReadDto> CreateCustomerAsync(CustomerCreateDto customerCreateDto);
        Task<bool> UpdateCustomerAsync(int id, CustomerUpdateDto customerUpdateDto);
        Task<bool> DeleteCustomerAsync(int id);
    }
}
