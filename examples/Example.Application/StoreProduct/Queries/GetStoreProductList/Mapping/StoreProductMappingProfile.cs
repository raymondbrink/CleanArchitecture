namespace Example.Application.StoreProduct.Queries.GetStoreProductList.Mapping
{
    using AutoMapper;

    using Domain.Entities;

    using Models;

    using NetActive.CleanArchitecture.Application.Extensions;

    internal class StoreProductMappingProfile : Profile
    {
        public StoreProductMappingProfile()
        {
            CreateMap<StoreProduct, StoreProductListModel>()
                .ForMember(d => d.Name, a => a.MapFromTranslation(s => s.Product.Translations, t => t.Name))
                .ForMember(d => d.Description, a => a.MapFromTranslation(s => s.Product.Translations, t => t.Description))
                .ForMember(m => m.IsAvailable, a => a.MapFrom(s => s.GetIsAvailable()))
                .ForMember(m => m.AvailableFrom, a => a.MapFrom(s => s.GetIsAvailableFrom()))
                .ForMember(m => m.AvailableUntil, a => a.MapFrom(s => s.GetIsAvailableUntil()))
                .ForMember(m => m.IsInStock, a => a.MapFrom(s => s.GetInStock()))
                .ForMember(m => m.Status, a => a.MapFrom(s => s.GetStatus()));
        }
    }
}