using CustomerXAPI.Dtos;

namespace CustomerXAPI.Interfaces
{
    public interface IPackageService
    {
        Task<PackageReadDto> GetPackageAsync(int id);
        Task<IEnumerable<PackageReadDto>> GetAllPackagesAsync();
        Task<PackageReadDto> CreatePackageAsync(PackageCreateDto packageCreateDto);
        Task<PackageReadDto> UpdatePackageAsync(int id, PackageUpdateDto packageUpdateDto);
        Task<bool> DeletePackageAsync(int id);
    }
}
