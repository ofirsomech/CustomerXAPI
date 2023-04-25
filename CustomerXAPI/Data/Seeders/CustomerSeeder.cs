using Bogus;
using CustomerXAPI.Enums;
using CustomerXAPI.Models;
using CustomerXAPI.Utilis;
using Microsoft.EntityFrameworkCore;

namespace CustomerXAPI.Data.Seeders
{
    public class CustomerSeeder : DbContextSeed
    {
        private int customerIdCounter = 1;
        private int contractIdCounter = 1;
        private int packageIdCounter = 1;

        public override async Task SeedAsync(DbContext context)
        {
            if (await context.Set<Customer>().AnyAsync())
            {
                return;
            }

            var subscriptionTypes = Enum.GetValues(typeof(eSubscriptionType)).Cast<eSubscriptionType>().ToList();
            var packageTypes = Enum.GetValues(typeof(ePackageType)).Cast<ePackageType>().ToList();

            var customerFaker = new Faker<Customer>("en_US")
              .RuleFor(c => c.ID, f => customerIdCounter++)
              .RuleFor(c => c.FirstName, f => f.Name.FirstName())
              .RuleFor(c => c.LastName, f => f.Name.LastName())
              .RuleFor(c => c.IdentityNumber, f => IdUtils.GenerateIsraeliIDNumber())
              .RuleFor(c => c.AddressCity, f => f.Address.City())
              .RuleFor(c => c.AddressStreet, f => f.Address.StreetName())
              .RuleFor(c => c.AddressHouseNumber, f => f.Address.BuildingNumber())
              .RuleFor(c => c.PostalCode, f => f.Address.ZipCode())
              .RuleFor(c => c.Contracts, f => new[] {
          new Contract {
            ID = contractIdCounter++,
              SubscriptionNumber = f.Random.Replace("#####"),
              SubscriberName = f.Name.FullName(),
              SubscriptionType = f.PickRandom(subscriptionTypes),
              Packages = f.Make(f.Random.Int(1, 10), () => new Package {
                ID = packageIdCounter++,
                  PackageType = f.PickRandom(packageTypes),
                  PackageName = f.Commerce.ProductName(),
                  Amount = f.Random.Int(50, 100),
                  Used = f.Random.Int(0, 50),
              }).ToArray()
          }
              });

            var customers = customerFaker.Generate(100);

            foreach (var customer in customers)
            {
                foreach (var contract in customer.Contracts)
                {
                    contract.SubscriberName = $"{customer.FirstName} {customer.LastName}";
                }
            }

            await context.Set<Customer>().AddRangeAsync(customers);
            await context.SaveChangesAsync();
        }
    }
}