namespace NetActive.CleanArchitecture.Autofac;

using global::Autofac;

/// <summary>
/// Can be used as a base for a Clean Architecture Application Entity module.
/// </summary>
public abstract class BaseModule : Module
{
    /// <summary>
    /// Constructor used to create a module base.
    /// </summary>
    /// <param name="registerSingleInstance">Boolean value indicating whether registrations should be single instance.</param>
    protected BaseModule(bool registerSingleInstance)
    {
        RegisterSingleInstance = registerSingleInstance;
    }

    /// <summary>
    /// Constructor used to create a module base.
    /// </summary>
    /// <param name="registrationParams">Dictionary of parameters to be used during registration.</param>
    /// <param name="registerSingleInstanceOnly">Boolean value indicating whether registrations should be single instance.</param>
    protected BaseModule(
        IDictionary<string, object> registrationParams,
        bool registerSingleInstanceOnly)
    {
        RegistrationParams = registrationParams;
        RegisterSingleInstance = registerSingleInstanceOnly;
    }

    /// <summary>
    /// Dictionary of parameters to be used during registration.
    /// </summary>
    protected IDictionary<string, object> RegistrationParams { get; } = null!;

    /// <summary>
    /// Boolean value indicating whether registrations should be single instance.
    /// </summary>
    protected bool RegisterSingleInstance { get; }
}