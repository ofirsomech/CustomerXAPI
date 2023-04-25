using AutoMapper.Execution;
using CustomerXAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IPI_server.Data
{
using AutoMapper.Execution;
using CustomerXAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IPI_server.Data
{
    public class CustomerXContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Package> Packages { get; set; }
        public CustomerXContext(DbContextOptions<CustomerXContext> options)
            : base(options)
        {
        }
    }
}

}
