using API.Controllers.Extensions;
using Application.Helpers.MappingProfiles;
using Application.Interfaces;
using Application.Posts;
using Application.Users;
using Domain;
using FluentValidation.AspNetCore;
using Infrastucture.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityService(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddScoped<IUserNamesList, UserList>();

// Add services to the container.
builder.Services.AddDbContext<DataContext>(optionsAction =>
{
    optionsAction.UseNpgsql(builder.Configuration.GetConnectionString("Postgre"));
});
builder.Services.AddMediatR(typeof(CreatePost.CreatePostCommand).Assembly);

builder.Services.AddAutoMapper(typeof(MappingProfiles));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCors", builder =>
    {
        builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    }
    );
});

var app = builder.Build();

load();

app.UseMiddleware<ErrorHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();
app.UseCors("MyCors");

app.MapControllers();

app.Run();


void load(){
    using var scope=app.Services.CreateScope();
    var userManager=scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    SeedData.SeedAsync(userManager);
}