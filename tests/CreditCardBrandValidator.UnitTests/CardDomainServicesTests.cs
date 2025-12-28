using CreditCardBrandValidator.Domain.Services;

namespace CreditCardBrandValidator.UnitTests;

public class CardDomainServicesTests
{
    private readonly CardDomainService _service;

    public CardDomainServicesTests()
    {
        _service = new CardDomainService();
    }

    [Theory]
    [InlineData("4081414902417", "Visa")]              // Visa 13 dígitos
    [InlineData("4842816925457031", "Visa")]           // Visa 16 dígitos
    [InlineData("5171317398219873", "Mastercard")]     // Mastercard
    [InlineData("6363686414712848", "Elo")]            // Elo (prioridade sobre Visa)
    [InlineData("344864126161912", "American Express")] // Amex (15 dígitos)
    [InlineData("6062823652093300", "Hipercard")]      // Hipercard
    public void ValidateCard_ValidNumbers_ShouldReturnSuccess(string cardNumber, string expectedBrand)
    {
        // Act
        var (isValid, brand) = _service.ValidateCard(cardNumber);

        // Assert
        Assert.True(isValid);
        Assert.Equal(expectedBrand, brand);
    }

    [Theory]
    [InlineData("4842-8169-2545-7031", "Visa")]       // Com traços
    [InlineData("5171.3173.9821.9873", "Mastercard")] // Com pontos
    [InlineData("  6363 6864 1471 2848  ", "Elo")]    // Com espaços
    public void ValidateCard_FormattedNumbers_ShouldCleanAndValidate(string cardNumber, string expectedBrand)
    {
        // Act
        var (isValid, brand) = _service.ValidateCard(cardNumber);

        // Assert
        Assert.True(isValid);
        Assert.Equal(expectedBrand, brand);
    }

    [Theory]
    [InlineData(null, "Unknown")]                   // Nulo
    [InlineData("", "Unknown")]                     // Vazio
    [InlineData("12345", "Unknown")]                // Muito curto (< 13)
    [InlineData("1234567890123456789012345678901", "Unknown")] // Muito longo (> 30 na entrada)
    [InlineData("4842816925457032", "Unknown")]    // Visa correta, mas falha no Luhn
    [InlineData("9999999999999999", "Unknown")]    // Luhn pode até passar, mas bandeira inexiste
    public void ValidateCard_InvalidInputs_ShouldReturnFalse(string? cardNumber, string expectedBrand)
    {
        // Act
        var (isValid, brand) = _service.ValidateCard(cardNumber);

        // Assert
        Assert.False(isValid);
        Assert.Equal(expectedBrand, brand);
    }
}