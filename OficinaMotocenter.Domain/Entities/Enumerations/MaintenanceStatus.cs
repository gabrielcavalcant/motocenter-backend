namespace OficinaMotocenter.Domain.Entities.Enumerations
{
    /// <summary>
    /// Represents the status of maintenance.
    /// </summary>
    public enum MaintenanceStatus : short
    {
        /// <summary>
        /// Created.
        /// </summary>
        Created = 1,

        /// <summary>
        /// InProgress.
        /// </summary>
        InProgress = 2,

        /// <summary>
        /// Done.
        /// </summary>
        Done = 3,
    }
}
