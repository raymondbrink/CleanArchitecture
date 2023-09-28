namespace NetActive.CleanArchitecture.Persistence.Configuration
{
    using Domain.Interfaces;

    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Options for configuring persistence dependencies.
    /// </summary>
    public class PersistenceOptionsBuilder
    {
        public IDictionary<Type, Type> EntityTypes { get; } = new Dictionary<Type, Type>();

        public IDictionary<Type, Type> ArchivableEntityTypes { get; } = new Dictionary<Type, Type>();

        /// <summary>
        /// Registers a repository for the specified entity type, with the given key type.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <typeparam name="TKey">Type of key.</typeparam>
        public void RegisterRepository<TEntity, TKey>()
            where TEntity : class, IEntity<TKey>, IAggregateRoot
            where TKey : struct
        {
            // Add to list of entity types to register repositories for.
            EntityTypes.Add(typeof(TEntity), typeof(TKey));
        }

        /// <summary>
        /// Registers a repository for the specified archivable entity type, with the given key type.
        /// </summary>
        /// <typeparam name="TArchivableEntity">Type of the archivable entity.</typeparam>
        /// <typeparam name="TKey">Type of key.</typeparam>
        public void RegisterArchivableRepository<TArchivableEntity, TKey>()
            where TArchivableEntity : class, IEntity<TKey>, IArchivableEntity, IAggregateRoot
            where TKey : struct
        {
            // Add to list of archivable entity types to register repositories for.
            ArchivableEntityTypes.Add(typeof(TArchivableEntity), typeof(TKey));
        }
    }
}