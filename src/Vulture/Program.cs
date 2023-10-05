using System.Text.Json.Serialization;
using Shared.Broker.Consumers;
using Shared.Contracts.Models;
using Shared.Entities;
using Shared.Extensions;
using Vulture.Consumers;
using Vulture.Extensions;
using Vulture.Workers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureOptions(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddVultureValidators();
builder.Services.AddBacenHttpClients(builder.Configuration);
builder.Services.ConfigureKafka(builder.Configuration, "Kafka")
    .AddPublishers<Entry>()
    .AddPublishers<Claim>()
    .AddPublishers<AddressingKeyForAccountModel>()
    .AddConsumer<AddressingKeyForAccountConsumer, AddressingKeyForAccountModel>()
    .AddConsumer<ClaimConsumer, Claim>();

builder.Services.AddHostedService<ClaimsWorker>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
