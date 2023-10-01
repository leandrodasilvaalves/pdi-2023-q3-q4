using Microsoft.Extensions.Options;
using Shared.Broker;
using Shared.Contracts.Repositories;
using Shared.Entities;
using Shared.Extensions;
using Shared.HttpClients;
using Shared.Requests;
using Vulture.Contracts;

namespace Vulture.Workers
{
    public class ClaimsWorker : BackgroundService
    {
        private readonly IBacenClaimClient _bacenClaimClient;
        private readonly IClaimRepository _repository;
        private readonly IPublisher<Claim> _publisher;
        private readonly KafkaTopcis _options;
        private readonly ILogger<ClaimsWorker> _logger;

        public ClaimsWorker(IBacenClaimClient bacenClaimClient,
                            IClaimRepository repository,
                            IPublisher<Claim> publisher,
                            IOptions<KafkaTopcis> options,
                            ILogger<ClaimsWorker> logger)
        {
            _bacenClaimClient = bacenClaimClient ?? throw new ArgumentNullException(nameof(bacenClaimClient));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.UtcNow;
                var result = await _bacenClaimClient.GetAsync(Constants.ISPB, new GetClaimsQueryParams(now.AddSeconds(-10), now));
                _logger.LogInformation("Resquested At: {0} \nResponse:{1}", now.ToString("yyyy-MM-ddTHH:mm:ss.fff"), result.Content.ToJson());

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
                        await _publisher.PublishAsync(_options.Claims, claim);
                    }
                }
                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }
    }
}