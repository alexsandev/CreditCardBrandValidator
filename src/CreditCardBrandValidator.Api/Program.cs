using CreditCardBrandValidator.Application.DTOs;
using CreditCardBrandValidator.Application.Interfaces;
using CreditCardBrandValidator.Application.Services;
using CreditCardBrandValidator.Domain.Interfaces;
using CreditCardBrandValidator.Domain.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<ICardAppService, CardAppService>();
builder.Services.AddScoped<ICardDomainService, CardDomainService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/ValidateCard", ([FromQuery]string cardNumber, [FromServices]ICardAppService cardAppService) =>
{
    if (string.IsNullOrEmpty(cardNumber))
    {
        return Results.BadRequest("Card number must be provided.");
    }

    var response = cardAppService.ExecuteCardValidation(new CardRequestDTO(cardNumber));
    
    return Results.Ok(response);
});

app.Run();

