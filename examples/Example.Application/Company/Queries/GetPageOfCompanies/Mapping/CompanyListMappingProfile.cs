namespace Example.Application.Company.Queries.GetPageOfCompanies.Mapping;

using AutoMapper;

using Domain.Entities;

using Models;

internal class CompanyListMappingProfile : Profile
{
    public CompanyListMappingProfile()
    {
        CreateMap<Company, CompanyListModel>();
    }
}