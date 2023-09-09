namespace NetActive.CleanArchitecture.Persistence.EntityFrameworkCore
{
    using System.Reflection;

    /// <summary>
    /// Provides an easy reference to the current assembly.
    /// </summary>
    public static class AssemblyReference
    {
        /// <summary>
        /// Gets the current assembly.
        /// </summary>
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
