namespace NetActive.CleanArchitecture.UseCase.FeatureName.Queries.UseCaseName.Mapping
{
    using AutoMapper;

    using Domain.Entities;
    using Models;

    internal class FeatureNameMappingProfile 
        : Profile
    {
        public FeatureNameMappingProfile()
        {
            CreateMap<FeatureName, FeatureNameListModel>();
        }
    }
}