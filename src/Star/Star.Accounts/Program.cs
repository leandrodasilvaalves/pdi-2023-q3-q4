using System.Text.Json.Serialization;
using Shared.Broker.Consumers;
using Shared.Contracts.Models;
using Shared.Extensions;

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
builder.Services.AddValidators();
builder.Services.AddBacenHttpClients(builder.Configuration);
builder.Services.ConfigureKafka(builder.Configuration, "Kafka")
    .AddConsumer<AddressingKeyForAccountConsumer, AddressingKeyForAccountModel>();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();
