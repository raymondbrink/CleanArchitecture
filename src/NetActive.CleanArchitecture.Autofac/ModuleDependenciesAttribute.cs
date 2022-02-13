namespace NetActive.CleanArchitecture.Autofac;

using System;

/// <summary>
///     The Module Dependencies Attribute can be used to mark other Modules as a dependency for the module this
///     attribute is placed on.
///     <remarks>
///         Note that when calling the RegisterModule extension method on the ContainerBuilder, it will also
///         automatically register dependency-modules if they haven't already.
///     </remarks>
/// </summary>
public class ModuleDependenciesAttribute : Attribute
{
    /// <summary>
    /// </summary>
    /// <param name="dependencies"></param>
    public ModuleDependenciesAttribute(params Type[] dependencies)
    {
        Dependencies = dependencies;
    }

    /// <summary>
    ///     The dependent module types.
    /// </summary>
    public Type[] Dependencies { get; }
}