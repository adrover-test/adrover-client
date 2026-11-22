using Adrover.Client.Backend.Options;
using Adrover.Client.Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services
    .AddOptions<IntelisaApiOptions>()
    .Bind(builder.Configuration.GetSection(IntelisaApiOptions.SectionName))
    .Validate(options => Uri.TryCreate(options.BaseUrl, UriKind.Absolute, out _), "IntelisaApi:BaseUrl must be a valid absolute URL.")
    .ValidateOnStart();

builder.Services.AddSingleton<ITokenStore, InMemoryTokenStore>();
builder.Services.AddHttpClient<IIntelisaApiService, IntelisaApiService>((serviceProvider, client) =>
{
    var options = serviceProvider.GetRequiredService<Microsoft.Extensions.Options.IOptions<IntelisaApiOptions>>().Value;
    client.BaseAddress = new Uri(options.BaseUrl);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
