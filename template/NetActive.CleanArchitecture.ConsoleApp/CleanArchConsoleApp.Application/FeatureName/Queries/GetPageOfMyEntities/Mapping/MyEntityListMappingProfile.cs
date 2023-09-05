namespace CleanArchConsoleApp.Application.FeatureName.Queries.GetPageOfMyEntities.Mapping
{
    using AutoMapper;

    using Domain.Entities;

    using Models;

    internal class FeatureNameListMappingProfile : Profile
    {
        public FeatureNameListMappingProfile()
        {
            CreateMap<FeatureName, FeatureNameListModel>();
        }
    }
}