namespace Example.Application.StoreProduct.Queries.GetStoreProductList.Models
{
    using NetActive.CleanArchitecture.Application.Interfaces;

    public class StoreProductListModel : IModel
    {
        public long Id { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Name { get; set; }

        public string Description { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public bool IsAvailable { get; set; }

        public DateTime? AvailableFrom { get; set; }

        public DateTime? AvailableUntil { get; set; }

        public bool IsInStock { get; set; }

        public int InStock { get; set; }

        public StoreProductStatusModel Status { get; set; }
    }
}