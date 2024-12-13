[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("NetActive.CleanArchitecture.Tests")]

namespace NetActive.CleanArchitecture.Domain.FluentValidation
{
    using System.Reflection;

    /// <summary>
    /// Provides an easy reference to the current assembly.
    /// </summary>
    internal static class AssemblyReference
    {
        /// <summary>
        /// Gets the current assembly.
        /// </summary>
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
