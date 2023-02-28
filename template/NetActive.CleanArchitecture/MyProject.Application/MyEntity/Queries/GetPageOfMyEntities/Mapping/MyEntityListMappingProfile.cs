namespace MyProject.Application.MyEntity.Queries.GetPageOfMyEntities.Mapping
{
    using AutoMapper;

    using Domain.Entities;

    using Models;

    internal class MyEntityListMappingProfile : Profile
    {
        public MyEntityListMappingProfile()
        {
            CreateMap<MyEntity, MyEntityListModel>();
        }
    }
}