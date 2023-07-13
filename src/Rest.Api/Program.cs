using Rest.Application.Repositories;
using Rest.Infrastructure.ModelBinders;
using Rest.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new IdModelBinderProvider());
    options.ModelBinderProviders.Insert(0, new ContinuationTokenModelBinderProvider());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IUnitOfWork, InMemoryUnitOfWork>();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    await new JsonSeeder().Seed(scope.ServiceProvider.GetRequiredService<IUnitOfWork>());
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
