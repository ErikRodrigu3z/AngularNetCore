using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Northwin.DataAccess;
using Northwind.Api.GlobalErrorHanding;
using Northwind.Api.HeaderFilter;
using Northwind.UnitOfWork;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region UnitOfWork context
builder.Services.AddScoped<IUnitOfWork>(opt => new UnitOfWork(builder.Configuration.GetConnectionString("Northwind")));
#endregion

#region JWT Authentication settings
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
#endregion

#region Controllers, EndpointsApiExplorer 

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
#endregion

#region Cors
var corsPolicy = "corsPolicy";
builder.Services.AddCors(op =>
{
    op.AddPolicy(name: corsPolicy,
            policy => {
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowAnyOrigin();
            });
});
#endregion

#region Swagger
builder.Services.AddSwaggerGen(opt => 
{
    opt.OperationFilter<HeaderFilter>();
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header. \r\n\r\n Enter the token in the text input below."
    });
});
#endregion

#region app build
var app = builder.Build();
#endregion

#region Configure the HTTP request pipeline, Environments
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#endregion

#region app settings
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.ConfigureExceptionHandler(); // errorHandler extension 
app.Run(); 
#endregion
