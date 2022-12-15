namespace NetActive.CleanArchitecture.Application.Models
{
    /// <summary>
    /// Contains one page of results from an entity query.
    /// </summary>
    public interface IPagedQueryResultModel
    {
        /// <summary>
        /// Gets the page requested index.
        /// </summary>
        public uint PageIndex { get; }

        /// <summary>
        /// Gets the page requested number.
        /// </summary>
        public uint PageNumber { get; }

        /// <summary>
        /// Returns a boolean value indicating whether there's another page of results available.
        /// </summary>
        bool HasNextPage();

        /// <summary>
        /// Gets the requested page size.
        /// </summary>
        public uint PageSize { get; }

        /// <summary>
        /// Gets the total number of pages.
        /// </summary>
        public uint PageCount { get; }

        /// <summary>
        /// Gets the total number of matching entities found.
        /// </summary>
        public ulong ItemCount { get; }
    }
}