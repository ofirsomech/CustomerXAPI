using CustomerXAPI.Dtos;

namespace CustomerXAPI.Interfaces
{
    public interface IContractService
    {
        Task<ContractReadDto> GetContractAsync(string subscriptionNumber);
        Task<IEnumerable<ContractReadDto>> GetAllContractsAsync();
        Task<ContractReadDto> CreateContractAsync(ContractCreateDto contractCreateDto);
        Task<ContractReadDto> UpdateContractAsync(string subscriptionNumber, ContractUpdateDto contractUpdateDto);
        Task<bool> DeleteContractAsync(string subscriptionNumber);
    }
}
