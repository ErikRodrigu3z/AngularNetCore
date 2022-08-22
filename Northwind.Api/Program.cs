using Microsoft.AspNetCore.Authentication.JwtBearer;
using Northwin.DataAccess;
using Northwind.Api.Auth;
using Northwind.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUnitOfWork>(opt => new UnitOfWork(
    builder.Configuration.GetConnectionString("Northwind")
));

var tokenProvider = new JwtProvider("issuer", "audience", "northwin_2000");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.RequireHttpsMetadata = false;
        opt.TokenValidationParameters = tokenProvider.GetValidationParameters();
    });
builder.Services.AddSingleton<ITokenProvider>(tokenProvider);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
    

app.MapControllers();

app.Run();
