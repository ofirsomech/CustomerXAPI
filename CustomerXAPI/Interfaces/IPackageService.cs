using CustomerXAPI.Dtos;

namespace CustomerXAPI.Interfaces
{
    public interface IPackageService
    {
        Task<IEnumerable<PackageReadDto>> GetAllPackagesAsync();
        Task<PackageReadDto> GetPackageByIdAsync(int id);
        Task<PackageReadDto> CreatePackageAsync(PackageCreateDto packageCreateDto);
        Task<bool> UpdatePackageAsync(int id, PackageUpdateDto packageUpdateDto);
        Task<bool> DeletePackageAsync(int id);
    }
}
