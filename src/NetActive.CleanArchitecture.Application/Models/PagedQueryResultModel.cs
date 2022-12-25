namespace NetActive.CleanArchitecture.Application.Models
{
    using System;
    using System.Collections.Generic;

    /// <inheritdoc cref="IPagedQueryResultModel" />
    public class PagedQueryResultModel<TModel> : IPagedQueryResultModel
    {
        public PagedQueryResultModel(uint pageIndex, uint pageSize, ulong itemCount, List<TModel> pageOfItems)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            ItemCount = itemCount;
            PageOfItems = pageOfItems;
        }

        /// <summary>
        /// Gets one page of matching items.
        /// </summary>
        public List<TModel> PageOfItems { get; }

        /// <inheritdoc />
        public uint PageIndex { get; }

        /// <inheritdoc />
        public uint PageNumber => PageIndex + 1;

        /// <inheritdoc />
        public bool HasNextPage()
        {
            return PageIndex < PageCount - 1;
        }

        /// <inheritdoc />
        public uint PageSize { get; }

        /// <inheritdoc />
        public uint PageCount => (uint)Math.Ceiling((double)ItemCount / PageSize);

        /// <inheritdoc />
        public ulong ItemCount { get; }
    }
}