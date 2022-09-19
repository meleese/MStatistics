using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MStatistics.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MStatistics.WebApi
{
    public class TwitterListener : BackgroundService
    {
        private readonly ILogger<TwitterListener> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public TwitterListener(ILogger<TwitterListener> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        /// <summary>
        /// Executes the background service that invokes the twitter sample stream
        /// </summary>
        /// <param name="cancellationToken">Aborts execution</param>
        /// <returns>Task</returns>
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Twitter listener service started...");

            try
            {
                await using AsyncServiceScope asyncScope = _scopeFactory.CreateAsyncScope();
                ITwitterService twitterService = asyncScope.ServiceProvider.GetRequiredService<ITwitterService>();
                await twitterService.Invoke(cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Failed to execute {nameof(TwitterListener)}. Message {e.Message}. Inner Exception: {e.InnerException}. Stack Trace: {e.StackTrace}");
            }
        }
    }
}