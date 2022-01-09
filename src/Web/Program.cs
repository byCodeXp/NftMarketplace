using Web.Extensions;
using Web.Middlewares;

// Add services to the container.

var builder = WebApplication.CreateBuilder(args);

{
    builder.InstallServicesInAssembly();
}

// Configure the HTTP request pipeline.

var app = builder.Build();

{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<ErrorsHandlerMiddleware>();

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
}

app.Seed();
app.Run();