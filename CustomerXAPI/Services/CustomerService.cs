using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CustomerXAPI.Dtos;
using CustomerXAPI.Interfaces;
using CustomerXAPI.Models;
using IPI_server.Data.IPI_server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace CustomerXAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerXContext _context;
        private readonly IMapper _mapper;

        public CustomerService(CustomerXContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CustomerReadDto>> GetAllCustomersAsync()
        {
            var customers = await _context.Customers
                .Include(c => c.Contracts)
                .ThenInclude(ct => ct.Packages)
                .ProjectTo<CustomerReadDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return customers;
        }

        public async Task<CustomerReadDto> GetCustomerByIdentityNumberAsync(string identityNumber)
        {
            var customer = await _context.Customers.Include(c => c.Contracts).ThenInclude(ct => ct.Packages).FirstOrDefaultAsync(c => c.IdentityNumber == identityNumber);
            return _mapper.Map<CustomerReadDto>(customer);
        }

        public async Task<CustomerReadDto> GetCustomerByIdAsync(int id)
        {
            var customer = await _context.Customers.Include(c => c.Contracts).ThenInclude(ct => ct.Packages).FirstOrDefaultAsync(c => c.ID == id);
            return _mapper.Map<CustomerReadDto>(customer);
        }

        public async Task<CustomerReadDto> CreateCustomerAsync(CustomerCreateDto customerCreateDto)
        {
            var customer = _mapper.Map<Customer>(customerCreateDto);
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return _mapper.Map<CustomerReadDto>(customer);
        }

        public async Task<CustomerReadDto> UpdateCustomerAddressAsync(int id, UpdateAddressDto updateAddressDto)
        {
            var customer = await _context.Customers
                .Include(c => c.Contracts)
                    .ThenInclude(ct => ct.Packages)
                .FirstOrDefaultAsync(c => c.ID == id);

            if (customer == null) return null;

            // Update customer address properties from UpdateAddressDto
            customer.AddressCity = updateAddressDto.AddressCity ?? customer.AddressCity;
            customer.AddressStreet = updateAddressDto.AddressStreet ?? customer.AddressStreet;
            customer.AddressHouseNumber = updateAddressDto.AddressHouseNumber ?? customer.AddressHouseNumber;
            customer.PostalCode = updateAddressDto.PostalCode ?? customer.PostalCode;

            await _context.SaveChangesAsync();

            var customerDto = _mapper.Map<CustomerReadDto>(customer);
            return customerDto;
        }

        public async Task<bool> UpdateCustomerAsync(int id, CustomerUpdateDto customerUpdateDto)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.ID == id);

            if (customer == null) return false;

            _mapper.Map(customerUpdateDto, customer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.ID == id);

            if (customer == null) return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}