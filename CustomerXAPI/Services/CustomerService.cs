using AutoMapper;
using CustomerXAPI.Dtos;
using CustomerXAPI.Interfaces;
using CustomerXAPI.Models;
using IPI_server.Data;
using Microsoft.EntityFrameworkCore;
using System;

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

        public async Task<CustomerReadDto> GetCustomerAsync(string id)
        {
            var customer = await _context.Customers
                .Include(c => c.Contracts)
                .FirstOrDefaultAsync(c => c.ID == id);

            if (customer == null)
            {
                return null;
            }

            return _mapper.Map<CustomerReadDto>(customer);
        }

        public async Task<IEnumerable<CustomerReadDto>> GetAllCustomersAsync()
        {
            var customers = await _context.Customers
                .Include(c => c.Contracts)
                .ToListAsync();

            return _mapper.Map<IEnumerable<CustomerReadDto>>(customers);
        }

        public async Task<CustomerReadDto> CreateCustomerAsync(CustomerCreateDto customerCreateDto)
        {
            var customer = _mapper.Map<Customer>(customerCreateDto);

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return _mapper.Map<CustomerReadDto>(customer);
        }

        public async Task<CustomerReadDto> UpdateCustomerAsync(string id, CustomerUpdateDto customerUpdateDto)
        {
            var customer = await _context.Customers
                .Include(c => c.Contracts)
                .FirstOrDefaultAsync(c => c.ID == id);

            if (customer == null)
            {
                return null;
            }

            _mapper.Map(customerUpdateDto, customer);

            await _context.SaveChangesAsync();

            return _mapper.Map<CustomerReadDto>(customer);
        }

        public async Task<bool> DeleteCustomerAsync(string id)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.ID == id);

            if (customer == null)
            {
                return false;
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return true;
        }
    }

}
