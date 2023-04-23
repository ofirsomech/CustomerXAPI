using CustomerXAPI.Dtos;

namespace CustomerXAPI.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerReadDto> GetCustomerAsync(string id);
        Task<IEnumerable<CustomerReadDto>> GetAllCustomersAsync();
        Task<CustomerReadDto> CreateCustomerAsync(CustomerCreateDto customerCreateDto);
        Task<CustomerReadDto> UpdateCustomerAsync(string id, CustomerUpdateDto customerUpdateDto);
        Task<bool> DeleteCustomerAsync(string id);
    }
}
