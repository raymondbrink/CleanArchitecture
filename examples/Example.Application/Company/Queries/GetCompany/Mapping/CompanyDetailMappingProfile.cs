namespace Example.Application.Company.Queries.GetCompany.Mapping
{
    using AutoMapper;

    using Domain.Entities;

    using Models;

    internal class CompanyDetailMappingProfile : Profile
    {
        public CompanyDetailMappingProfile()
        {
            CreateMap<Company, CompanyDetailModel>();
        }
    }
}