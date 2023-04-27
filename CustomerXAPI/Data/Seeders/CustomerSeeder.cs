using Bogus;
using CustomerXAPI.Enums;
using CustomerXAPI.Models;
using IsraeliHebrewNames;
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
              .RuleFor(c => c.FirstName, f => IsraeliDataGenerator.GetRandomFirstName())
              .RuleFor(c => c.LastName, f => IsraeliDataGenerator.GetRandomLastName())
              .RuleFor(c => c.IdentityNumber, (f, c) => c.ID == 1 ? "329756399" : IsraeliDataGenerator.GenerateIsraeliIDNumber())
              .RuleFor(c => c.AddressCity, f => IsraeliDataGenerator.GetRandomCity())
              .RuleFor(c => c.AddressStreet, f => IsraeliDataGenerator.GetRandomStreet())
              .RuleFor(c => c.AddressHouseNumber, f => IsraeliDataGenerator.GenerateHouseNumber())
              .RuleFor(c => c.PostalCode, f => IsraeliDataGenerator.GenerateIsraeliZipCode())
              .RuleFor(c => c.Contracts, f => {
                  var numContracts = f.Random.Int(1, 4);
                  return f.Make(numContracts, () => new Contract
                  {
                      ID = contractIdCounter++,
                      SubscriptionNumber = f.Random.Replace("#####"),
                      SubscriberName = f.Name.FullName(),
                      SubscriptionType = f.PickRandom(subscriptionTypes),
                      Packages = f.Make(f.Random.Int(1, 4), () => new Package
                      {
                          ID = packageIdCounter++,
                          PackageType = f.PickRandom(packageTypes),
                          PackageName = IsraeliDataGenerator.GetRandomPackageName(),
                          Amount = f.Random.Int(50, 100),
                          Used = f.Random.Int(0, 50),
                      }).ToArray()
                  }).ToArray();
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