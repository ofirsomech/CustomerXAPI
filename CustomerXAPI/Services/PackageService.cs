using AutoMapper;
using CustomerXAPI.Dtos;
using CustomerXAPI.Interfaces;
using CustomerXAPI.Models;
using IPI_server.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace CustomerXAPI.Services
{
    public class PackageService : IPackageService
    {
        private readonly CustomerXContext _context;
        private readonly IMapper _mapper;

        public PackageService(CustomerXContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PackageReadDto> GetPackageAsync(int id)
        {
            var package = await _context.Packages
                .FirstOrDefaultAsync(p => p.ID == id);

            if (package == null)
            {
                return null;
            }

            return _mapper.Map<PackageReadDto>(package);
        }

        public async Task<IEnumerable<PackageReadDto>> GetAllPackagesAsync()
        {
            var packages = await _context.Packages.ToListAsync();

            return _mapper.Map<IEnumerable<PackageReadDto>>(packages);
        }

        public async Task<PackageReadDto> CreatePackageAsync(PackageCreateDto packageCreateDto)
        {
            var package = _mapper.Map<Package>(packageCreateDto);

            await _context.Packages.AddAsync(package);
            await _context.SaveChangesAsync();

            return _mapper.Map<PackageReadDto>(package);
        }

        public async Task<PackageReadDto> UpdatePackageAsync(int id, PackageUpdateDto packageUpdateDto)
        {
            var package = await _context.Packages
                .FirstOrDefaultAsync(p => p.ID == id);

            if (package == null)
            {
                return null;
            }

            _mapper.Map(packageUpdateDto, package);

            await _context.SaveChangesAsync();

            return _mapper.Map<PackageReadDto>(package);
        }

        public async Task<bool> DeletePackageAsync(int id)
        {
            var package = await _context.Packages
                .FirstOrDefaultAsync(p => p.ID == id);

            if (package == null)
            {
                return false;
            }

            _context.Packages.Remove(package);
            await _context.SaveChangesAsync();

            return true;
        }
    }

}
