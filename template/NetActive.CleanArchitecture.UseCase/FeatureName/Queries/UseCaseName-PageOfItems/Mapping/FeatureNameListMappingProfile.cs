namespace NetActive.CleanArchitecture.UseCase.FeatureName.Queries.UseCaseName.Mapping
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