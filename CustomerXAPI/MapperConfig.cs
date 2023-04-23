using AutoMapper;
using CustomerXAPI.Dtos;
using CustomerXAPI.Models;

public class MapperConfig
{
    public static IMapper GetMapper()
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            // Customer
            mc.CreateMap<Customer, CustomerReadDto>();
            mc.CreateMap<CustomerCreateDto, Customer>();
            mc.CreateMap<CustomerUpdateDto, Customer>();
            // Contract
            mc.CreateMap<Contract, ContractReadDto>();
            mc.CreateMap<ContractCreateDto, Contract>();
            mc.CreateMap<ContractUpdateDto, Contract>();
            // Package
            mc.CreateMap<Package, PackageReadDto>();
            mc.CreateMap<PackageCreateDto, Package>();
            mc.CreateMap<PackageUpdateDto, Package>();
        });

        IMapper mapper = mappingConfig.CreateMapper();
        return mapper;
    }
}
