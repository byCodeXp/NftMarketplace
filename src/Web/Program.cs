using Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.InstallServicesInAssembly();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.ConfigureHttpRequestPipeline();

app.Seed();
app.Run();