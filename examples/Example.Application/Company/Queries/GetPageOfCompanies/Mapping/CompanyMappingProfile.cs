namespace Example.Application.Company.Queries.GetPageOfCompanies.Mapping;

using AutoMapper;

using Domain.Entities;

using Models;

internal class CompanyMappingProfile : Profile
{
    public CompanyMappingProfile()
    {
        CreateMap<Company, CompanyListModel>();
    }
}