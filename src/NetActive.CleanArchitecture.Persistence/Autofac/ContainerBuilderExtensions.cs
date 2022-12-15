namespace NetActive.CleanArchitecture.Persistence.Autofac
{
    using Application.Interfaces;

    using global::Autofac;
    using global::Autofac.Builder;

    public static class ContainerBuilderExtensions
    {
        /// <summary>
        /// Registers the specified unit of work as <see cref="IUnitOfWork"/>.
        /// </summary>
        /// <typeparam name="TUnitOfWork">The type of the unit of work.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="registerSingleInstance">Boolean value indicating whether a single instance should be used instead of an instance per lifetime scope.</param>
        public static IRegistrationBuilder<TUnitOfWork, ConcreteReflectionActivatorData, SingleRegistrationStyle>
            RegisterUnitOfWork<TUnitOfWork>(this ContainerBuilder builder, bool registerSingleInstance)
            where TUnitOfWork : IUnitOfWork
        {
            return builder.RegisterUnitOfWork<IUnitOfWork, TUnitOfWork>(registerSingleInstance);
        }

        /// <summary>
        /// Registers the specified unit of work.
        /// </summary>
        /// <typeparam name="TUnitOfWork">The type of the unit of work.</typeparam>
        /// <typeparam name="TIUnitOfWork">The interface extension of <see cref="IUnitOfWork"/> to register as.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="registerSingleInstance">Boolean value indicating whether a single instance should be used instead of an instance per lifetime scope.</param>
        public static IRegistrationBuilder<TUnitOfWork, ConcreteReflectionActivatorData, SingleRegistrationStyle>
            RegisterUnitOfWork<TIUnitOfWork, TUnitOfWork>(this ContainerBuilder builder, bool registerSingleInstance)
            where TIUnitOfWork : IUnitOfWork where TUnitOfWork : TIUnitOfWork
        {
            var registration = builder.RegisterType<TUnitOfWork>().As<TIUnitOfWork>();

            return registerSingleInstance ? registration.SingleInstance() : registration.InstancePerLifetimeScope();
        }
    }
}