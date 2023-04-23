using AutoMapper;
using CustomerXAPI.Dtos;
using CustomerXAPI.Interfaces;
using CustomerXAPI.Models;
using IPI_server.Data;
using Microsoft.EntityFrameworkCore;
using System;

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

        public async Task<ContractReadDto> GetContractAsync(string subscriptionNumber)
        {
            var contract = await _context.Contracts
                .Include(c => c.Packages)
                .FirstOrDefaultAsync(c => c.SubscriptionNumber == subscriptionNumber);

            if (contract == null)
            {
                return null;
            }

            return _mapper.Map<ContractReadDto>(contract);
        }

        public async Task<IEnumerable<ContractReadDto>> GetAllContractsAsync()
        {
            var contracts = await _context.Contracts
                .Include(c => c.Packages)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ContractReadDto>>(contracts);
        }

        public async Task<ContractReadDto> CreateContractAsync(ContractCreateDto contractCreateDto)
        {
            var contract = _mapper.Map<Contract>(contractCreateDto);

            contract.Packages = await _context.Packages
                .Where(p => contractCreateDto.PackageIds.Contains(p.ID))
                .ToListAsync();

            await _context.Contracts.AddAsync(contract);
            await _context.SaveChangesAsync();

            return _mapper.Map<ContractReadDto>(contract);
        }

        public async Task<ContractReadDto> UpdateContractAsync(string subscriptionNumber, ContractUpdateDto contractUpdateDto)
        {
            var contract = await _context.Contracts
                .Include(c => c.Packages)
                .FirstOrDefaultAsync(c => c.SubscriptionNumber == subscriptionNumber);

            if (contract == null)
            {
                return null;
            }

            _mapper.Map(contractUpdateDto, contract);
            contract.Packages = await _context.Packages
                .Where(p => contractUpdateDto.PackageIds.Contains(p.ID))
                .ToListAsync();

            await _context.SaveChangesAsync();

            return _mapper.Map<ContractReadDto>(contract);
        }

        public async Task<bool> DeleteContractAsync(string subscriptionNumber)
        {
            var contract = await _context.Contracts
                .FirstOrDefaultAsync(c => c.SubscriptionNumber == subscriptionNumber);

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
