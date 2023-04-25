using AutoMapper;
using CustomerXAPI.Data;
using CustomerXAPI.Dtos;
using CustomerXAPI.Interfaces;
using CustomerXAPI.Models;
using IPI_server.Data.IPI_server.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerXAPI.Services
{
    public class ContractService : IContractService
    {
        private readonly CustomerXContext _context;
        private readonly IMapper _mapper;

        public ContractService(CustomerXContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContractReadDto>> GetAllContractsAsync()
        {
            var contracts = await _context.Contracts.Include(c => c.Packages).ToListAsync();
            return _mapper.Map<IEnumerable<ContractReadDto>>(contracts);
        }

        public async Task<ContractReadDto> GetContractByIdAsync(int id)
        {
            var contract = await _context.Contracts.Include(c => c.Packages).FirstOrDefaultAsync(c => c.ID == id);

            if (contract == null)
            {
                return null;
            }

            return _mapper.Map<ContractReadDto>(contract);
        }

        public async Task<ContractReadDto> CreateContractAsync(ContractCreateDto contractCreateDto)
        {
            var contract = _mapper.Map<Contract>(contractCreateDto);

            contract.ID = 1;

            var packages = await _context.Packages.Where(p => contractCreateDto.PackageIds.Contains(p.ID)).ToListAsync();
            contract.Packages = packages;

            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();

            return _mapper.Map<ContractReadDto>(contract);
        }

        public async Task<bool> UpdateContractAsync(int id, ContractUpdateDto contractUpdateDto)
        {
            var contract = await _context.Contracts.Include(c => c.Packages).FirstOrDefaultAsync(c => c.ID == id);

            if (contract == null)
            {
                return false;
            }

            _mapper.Map(contractUpdateDto, contract);

            var packages = await _context.Packages.Where(p => contractUpdateDto.PackageIds.Contains(p.ID)).ToListAsync();
            contract.Packages = packages;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteContractAsync(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);

            if (contract == null)
            {
                return false;
            }

            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
