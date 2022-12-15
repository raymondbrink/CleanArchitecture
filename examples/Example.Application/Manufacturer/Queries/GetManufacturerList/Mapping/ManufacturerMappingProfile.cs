namespace Example.Application.Manufacturer.Queries.GetManufacturerList.Mapping
{
    using AutoMapper;

    using Domain.Entities;

    using Models;

    using Manufacturer = Domain.Entities.Manufacturer;

    internal class ManufacturerMappingProfile : Profile
    {
        public ManufacturerMappingProfile()
        {
            CreateMap<Company, CompanyListModel>();
            CreateMap<Manufacturer, ManufacturerListModel>()
                .IncludeBase<Company, CompanyListModel>();
            CreateMap<Person, PersonModel>();
        }
    }
}