namespace NetActive.CleanArchitecture.UseCase.FeatureName.Queries.UseCaseName.Mapping
{
    using AutoMapper;

    using Domain.Entities;

    using Models;

    internal class FeatureNameDetailMappingProfile : Profile
    {
        public FeatureNameDetailMappingProfile()
        {
            CreateMap<FeatureName, FeatureNameDetailModel>();
        }
    }
}