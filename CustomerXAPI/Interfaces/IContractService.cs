using CustomerXAPI.Dtos;

namespace CustomerXAPI.Interfaces
{
    public interface IContractService
    {
        Task<IEnumerable<ContractReadDto>> GetAllContractsAsync();
        Task<ContractReadDto> GetContractByIdAsync(int id);
        Task<ContractReadDto> CreateContractAsync(ContractCreateDto contractCreateDto);
        Task<bool> UpdateContractAsync(int id, ContractUpdateDto contractUpdateDto);
        Task<bool> DeleteContractAsync(int id);
    }
}
