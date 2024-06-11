using ContactApplication.API.Extention;
using ContactApplication.API.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(option =>
{
    option.AddPolicy("CorsPolicy",
        builder => builder.WithOrigins("http://localhost:4200")
        .AllowAnyMethod().
        AllowAnyHeader());
});

// Add services to the container.

builder.Services.AddControllers().ConfigureApiBehaviorOptions( option =>
{
    option.InvalidModelStateResponseFactory = acionContext =>
    {
        var modelState = acionContext.ModelState.Values;

        return new BadRequestObjectResult(new ErrorResponse
        {
            StatusCode = HttpStatusCode.BadRequest,
            Title = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
            Error = modelState.SelectMany(x => x.Errors, (x,y) => y.ErrorMessage).ToList(),
        });;
    };
});

builder.Services.AddApplicationService();

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

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
