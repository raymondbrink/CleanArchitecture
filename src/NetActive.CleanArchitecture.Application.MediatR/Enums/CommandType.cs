namespace NetActive.CleanArchitecture.Application.MediatR.Enums
{
    /// <summary>
    /// Type of command that was executed.
    /// </summary>
    public enum CommandType
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown,

        /// <summary>
        /// Create entity command.
        /// </summary>
        Create,

        /// <summary>
        /// Update entity command.
        /// </summary>
        Update,

        /// <summary>
        /// Delete entity command.
        /// </summary>
        Delete
    }
}
