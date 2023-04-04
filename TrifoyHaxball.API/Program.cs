using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using NLog.Web;
using System.Reflection;
using TrifoyHaxball.API.Filters;
using TrifoyHaxball.API.Middlewares;
using TrifoyHaxball.API.Modules;
using TrifoyHaxball.Core.Repositories;
using TrifoyHaxball.Core.Services;
using TrifoyHaxball.Core.UnitOfWorks;
using TrifoyHaxball.Entity;
using TrifoyHaxball.Repository;
using TrifoyHaxball.Repository.Repositories;
using TrifoyHaxball.Repository.UnitOfWorks;
using TrifoyHaxball.Service.Mapping;
using TrifoyHaxball.Service.Services;
using TrifoyHaxball.Service.Validations;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Host.UseNLog();


builder.Services.AddCors(opts =>
{
    opts.AddPolicy("AllowEveryone", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod();
    });

    opts.AddPolicy("AllowOwner", builder =>
    {
        builder.WithOrigins("https://localhost:7148").AllowAnyMethod().AllowAnyHeader();
    });

    
});

builder.Services.AddScoped<CheckWhiteListFilter>();
builder.Services.Configure<IPList>(builder.Configuration.GetSection("IPList"));



builder.Services.AddControllers(
options =>
{
    options.Filters.Add(new ValidateFilterAttribute());
}).AddFluentValidation(x =>
{
    x.RegisterValidatorsFromAssemblyContaining<PlayerSaveDtoValidator>();
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddDbContext<AppDbContext>(x=>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"),option=>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder=>containerBuilder.RegisterModule(new RepoServiceModule()));
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder=>containerBuilder.RegisterModule(new FilterModule()));



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();





app.UseCustomException();


app.UseAuthorization();

app.MapControllers();

app.Run();
