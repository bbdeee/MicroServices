using MicroServicesProject.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register ProfileService
builder.Services.AddScoped<ProfileService>();

// Register DbContext for ProfileContext
builder.Services.AddDbContext<ProfileContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Register MessageService as a hosted service and as a singleton for dependency injection
builder.Services.AddSingleton<MessageService>();  // Register as a Singleton
builder.Services.AddHostedService(provider => provider.GetRequiredService<MessageService>());  // Register for HostedService

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

