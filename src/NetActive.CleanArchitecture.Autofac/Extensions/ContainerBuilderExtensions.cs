namespace NetActive.CleanArchitecture.Autofac.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;

using global::Autofac;
using global::Autofac.Builder;
using global::Autofac.Core;
using global::Autofac.Core.Registration;

public static class ContainerBuilderExtensions
{
    /// <summary>
    /// Registers the given service.
    /// </summary>
    /// <typeparam name="TServiceInterface">The type of the service interface.</typeparam>
    /// <typeparam name="TService">The type of the service.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <param name="registerSingleInstance">
    ///     Boolean value indicating whether a single instance should be used instead of an
    ///     instance per request.
    /// </param>
    public static IRegistrationBuilder<TService, ConcreteReflectionActivatorData, SingleRegistrationStyle>
        RegisterService<TServiceInterface, TService>(this ContainerBuilder builder, bool registerSingleInstance)
        where TServiceInterface : class 
        where TService : TServiceInterface
    {
        // Register the service as interface
        var registration = builder.RegisterType<TService>().As<TServiceInterface>();
        return registerSingleInstance ? registration.SingleInstance() : registration.InstancePerLifetimeScope();
    }

    /// <summary>
    ///     Registers the given module.
    ///     <remarks>
    ///         This will also register Module Dependencies that are specified in the <see cref="ModuleDependenciesAttribute" />.
    ///     </remarks>
    /// </summary>
    /// <typeparam name="TModule">The type of the module.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <param name="serviceParams">Optional dictionary of parameters required for services in this module (or any of its dependent modules).</param>
    /// <param name="registerSingleInstance">if set to <c>true</c> [single instance service registration].</param>
    /// <returns></returns>
    public static IModuleRegistrar RegisterModule<TModule>(
        this ContainerBuilder builder,
        bool registerSingleInstance,
        IDictionary<string, object> serviceParams = null)
        where TModule : BaseModule
    {
        return registerModuleWithDependencies(builder, typeof(TModule), registerSingleInstance, serviceParams);
    }

    /// <summary>
    ///     Registers the given module.
    ///     <remarks>
    ///         This will also register Module Dependencies that are specified in the <see cref="ModuleDependenciesAttribute" />.
    ///     </remarks>
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="moduleType">Type of the module.</param>
    /// <param name="serviceParams">Optional dictionary of parameters required for services in this module (or any of its dependent modules).</param>
    /// <param name="registerSingleInstance">if set to <c>true</c> [single instance service registration].</param>
    /// <returns></returns>
    private static IModuleRegistrar registerModuleWithDependencies(
        this ContainerBuilder builder,
        Type moduleType,
        bool registerSingleInstance,
        IDictionary<string, object> serviceParams)
    {
        if (!typeof(BaseModule).IsAssignableFrom(moduleType))
        {
            throw new NotSupportedException(
                $"The type {moduleType.Name} is not assignable from {nameof(BaseModule)} and therefor cannot be passed to this method.");
        }

        // Register any module dependencies.
        var dependenciesAttributes = moduleType.GetCustomAttributes(typeof(ModuleDependenciesAttribute), true)
            .Cast<ModuleDependenciesAttribute>();
        foreach (var dependenciesAttribute in dependenciesAttributes)
        {
            foreach (var dependency in dependenciesAttribute.Dependencies)
            {
                builder.registerModuleWithDependencies(dependency, registerSingleInstance, serviceParams);
            }
        }

        // Register the module itself.
        var arguments = serviceParams != null ? new object[] { serviceParams, registerSingleInstance } : new object[] { registerSingleInstance };
        var instance = Activator.CreateInstance(moduleType, arguments) as IModule;

        return builder.RegisterModule(instance);
    }
}