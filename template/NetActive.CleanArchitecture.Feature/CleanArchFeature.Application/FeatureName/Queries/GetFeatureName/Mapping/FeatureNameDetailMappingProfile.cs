namespace CleanArchFeature.Application.FeatureName.Queries.GetFeatureName.Mapping
{
    using AutoMapper;

    using Domain.Entities;

    using Models;

    internal class FeatureNameDetailMappingProfile 
        : Profile
    {
        public FeatureNameDetailMappingProfile()
        {
            CreateMap<FeatureName, FeatureNameDetailModel>();
        }
    }
}