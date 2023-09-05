namespace NetActive.CleanArchitecture.Persistence.EntityFrameworkCore.Configuration
{
    using Domain.Interfaces;

    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Options for configuring persistence dependencies.
    /// </summary>
    public class PersistenceOptions
    {
        internal IDictionary<Type, Type> EntityTypes { get; } = new Dictionary<Type, Type>();

        internal IDictionary<Type, Type> ArchivableEntityTypes { get; } = new Dictionary<Type, Type>();

        /// <summary>
        /// Registers an EF repository for the specified entity type, with the given key type.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <typeparam name="TKey">Type of key.</typeparam>
        public void RegisterEfRepository<TEntity, TKey>()
            where TEntity : class, IEntity<TKey>, IAggregateRoot
            where TKey : struct
        {
            // Add to list of entity types to register repositories for.
            EntityTypes.Add(typeof(TEntity), typeof(TKey));
        }

        /// <summary>
        /// Registers an EF repository for the specified archivable entity type, with the given key type.
        /// </summary>
        /// <typeparam name="TArchivableEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        public void RegisterArchivableEfRepository<TArchivableEntity, TKey>()
            where TArchivableEntity : class, IEntity<TKey>, IArchivableEntity, IAggregateRoot
            where TKey : struct
        {
            // Add to list of entity types to register repositories for.
            ArchivableEntityTypes.Add(typeof(TArchivableEntity), typeof(TKey));
        }
    }
}