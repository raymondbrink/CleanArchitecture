namespace Example.Application.Customer.Queries.GetCustomerList.Mapping;

using AutoMapper;

using Domain.Entities;

using Models;

internal class CustomerMappingProfile : Profile
{
    public CustomerMappingProfile()
    {
        CreateMap<Customer, CustomerListModel>();
    }
}