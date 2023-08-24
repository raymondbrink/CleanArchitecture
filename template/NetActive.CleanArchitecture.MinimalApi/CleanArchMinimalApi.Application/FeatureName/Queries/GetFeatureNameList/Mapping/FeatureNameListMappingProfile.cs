namespace CleanArchMinimalApi.Application.FeatureName.Queries.GetFeatureNameList.Mapping
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