using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.Broker;
using Shared.Contracts.Options;
using Shared.Contracts.Repositories;
using Shared.Entities;
using Shared.Extensions;
using Shared.HttpClients;
using Shared.Requests;

namespace Shared.Workers
{
    public class ClaimsWorker : BackgroundService
    {
        private readonly IBacenClaimClient _bacenClaimClient;
        private readonly IClaimRepository _repository;
        private readonly IPublisher<Claim> _publisher;
        private readonly KafkaTopcis _topicsOptions;
        private readonly WorkerOptions _workerOptions;
        private readonly ILogger<ClaimsWorker> _logger;

        public ClaimsWorker(IBacenClaimClient bacenClaimClient,
                            IClaimRepository repository,
                            IPublisher<Claim> publisher,
                            IOptions<KafkaTopcis> topicsOptions,
                            IOptions<WorkerOptions> workerOptions,
                            ILogger<ClaimsWorker> logger)
        {
            _bacenClaimClient = bacenClaimClient ?? throw new ArgumentNullException(nameof(bacenClaimClient));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
            _topicsOptions = topicsOptions.Value ?? throw new ArgumentNullException(nameof(topicsOptions));
            _workerOptions = workerOptions.Value ?? throw new ArgumentNullException(nameof(workerOptions));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.UtcNow;
                var query = new GetClaimsQueryParams(now.AddSeconds(_workerOptions.IntervalInSeconds * -1), now);
                var result = await _bacenClaimClient.GetAsync(_workerOptions.Ispb, query);
                _logger.LogInformation("Resquested At: {0} - {1} \nResponse:{2}",
                    query.StartDate, query.EndDate, result.Content?.ToJson());

                if (result.Content.Data.Any())
                {
                    foreach (var claim in result.Content.Data)
                    {
                        var exsits = await _repository.ExistsAsync(claim.Id);
                        var processClaimAsync = exsits switch
                        {
                            false => _repository.InsertAsync(claim),
                            true => _repository.UpdateAsync(claim),
                        };
                        await processClaimAsync;
                        await _publisher.PublishAsync(_topicsOptions.Claims, claim);
                    }
                }
                await Task.Delay(TimeSpan.FromSeconds(_workerOptions.IntervalInSeconds), stoppingToken);
            }
        }
    }
}