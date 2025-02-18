using Microsoft.EntityFrameworkCore;
using MoncrestEmployeeProject.Data;
using MoncrestEmployeeProject.Repository;
using MoncrestEmployeeProject.Repository.Interface;
using MoncrestEmployeeProject.Repository.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// ✅ Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Frontend URL
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials(); // Enable only if needed
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Database Connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MoncrestConnectionString"));
});

// Register Repositories
builder.Services.AddScoped<IEmployee, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeDetails, EmployeeDetailRepository>();
builder.Services.AddScoped<IEmployeeNetworkIps, EmployeeNetworkIpsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ✅ Apply the CORS Policy
app.UseCors("AllowLocalhost");

app.UseAuthorization();
app.MapControllers();

app.Run();
