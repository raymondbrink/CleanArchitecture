namespace Example.Application.Supplier.Queries.GetSupplierList.Mapping;

using AutoMapper;

using Domain.Entities;

using Models;

using Supplier = Domain.Entities.Supplier;

internal class SupplierMappingProfile : Profile
{
    public SupplierMappingProfile()
    {
        CreateMap<Company, CompanyListModel>();
        CreateMap<Supplier, SupplierListModel>()
            .IncludeBase<Company, CompanyListModel>();
    }
}