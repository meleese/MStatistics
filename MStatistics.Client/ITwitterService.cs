namespace MStatistics.Client
{
    public interface ITwitterService
    {
        /// <summary>
        /// Calls the twitter sample stream service and records results locally.
        /// </summary>
        /// <param name="cancellationToken">Task cancellation token</param>
        /// <returns>Task</returns>
        Task Invoke(CancellationToken cancellationToken);
    }
}
