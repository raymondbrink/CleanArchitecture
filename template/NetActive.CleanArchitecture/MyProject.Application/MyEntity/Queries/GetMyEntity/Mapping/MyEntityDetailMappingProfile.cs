namespace MyProject.Application.MyEntity.Queries.GetMyEntity.Mapping
{
    using AutoMapper;

    using Domain.Entities;

    using Models;

    internal class MyEntityDetailMappingProfile : Profile
    {
        public MyEntityDetailMappingProfile()
        {
            CreateMap<MyEntity, MyEntityDetailModel>();
        }
    }
}