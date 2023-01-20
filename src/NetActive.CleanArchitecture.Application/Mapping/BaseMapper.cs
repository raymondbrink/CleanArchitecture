namespace NetActive.CleanArchitecture.Application.Mapping
{
    using System;

    using AutoMapper;
    using AutoMapper.Internal;

    /// <summary>
    /// Base for a lazy loading mapper adding a generic AutoMapper profile.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public abstract class BaseMapper<TProfile> where TProfile : Profile, new()
    {
        private static readonly Lazy<IMapper> LazyInstance = new(createMapper);

        /// <summary>
        /// Get the mapper instance. If not initialized yet, it's initialized first.
        /// </summary>
        public static IMapper Instance => LazyInstance.Value;

        private static IMapper createMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TProfile>();
                cfg.Internal().MethodMappingEnabled = false;
            });

            config.AssertConfigurationIsValid();
            config.CompileMappings();

            return config.CreateMapper();
        }
    }
}