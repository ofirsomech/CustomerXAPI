using CustomerXAPI.Dtos;
using CustomerXAPI.Interfaces;
using CustomerXAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomerXAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private readonly IPackageService _packageService;

        public PackagesController(IPackageService packageService)
        {
            _packageService = packageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPackages()
        {
            var packages = await _packageService.GetAllPackagesAsync();
            return Ok(packages);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPackageById(int id)
        {
            var package = await _packageService.GetPackageByIdAsync(id);

            if (package == null)
            {
                return NotFound();
            }

            return Ok(package);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePackage(PackageCreateDto packageCreateDto)
        {
            var package = await _packageService.CreatePackageAsync(packageCreateDto);
            return CreatedAtAction(nameof(GetPackageById), new { id = package.ID }, package);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePackage(int id, PackageUpdateDto packageUpdateDto)
        {
            var result = await _packageService.UpdatePackageAsync(id, packageUpdateDto);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackage(int id)
        {
            var result = await _packageService.DeletePackageAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
