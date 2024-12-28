
using CarManagmentApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:8088");

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddDbContext<CarManagmentAppContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CarManagmentAppContext>();

    var pendingMigrations = dbContext.Database.GetPendingMigrations();
    if (pendingMigrations.Any())
    {
        dbContext.Database.Migrate();
    }
}

app.UseStaticFiles();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corspolicy");

app.MapControllers();

app.Run();
