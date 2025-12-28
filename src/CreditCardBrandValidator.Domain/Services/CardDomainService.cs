using CreditCardBrandValidator.Domain.Interfaces;
using System.Text.RegularExpressions;

namespace CreditCardBrandValidator.Domain.Services;

public class CardDomainService : ICardDomainService
{
    private static readonly List<(string pattern, string brand)> CardPatterns = new()
    {
        (@"^((457631|457632|438935|636368|636297|504175|5067|5090|627780|636297)[0-9]*)", "Elo"),
        (@"^4[0-9]{12}(?:[0-9]{3})?$", "Visa"),
        (@"^5[1-5][0-9]{14}$|^2(?:22[1-9]|2[3-9][0-9]|[3-6][0-9]{2}|7[01][0-9]|720)[0-9]{12}$", "Mastercard"),
        (@"^3[47][0-9]{13}$", "American Express"),
        (@"^606282[0-9]{10}$|^3841[0-9]{15}$", "Hipercard")
    };

    public (bool isValid, string brand) ValidateCard(string? cardNumber)
    {
        if (string.IsNullOrWhiteSpace(cardNumber) || cardNumber.Length < 13 || cardNumber.Length > 30)
            return (false, "Unknown");

        var cleanNumber = Regex.Replace(cardNumber, @"[^\d]", "");

        if (cleanNumber.Length < 13 || cleanNumber.Length > 50)
            return (false, "Unknown");

        var brand = GetCardBrand(cleanNumber);
        var isValidLuhn = ValidateLuhn(cleanNumber);

        if (!isValidLuhn || brand == "Unknown")
            return (false, "Unknown");

        return (true, brand);
    }

    private string GetCardBrand(string cardNumber)
    {
        foreach (var (pattern, brand) in CardPatterns)
        {
            if (Regex.IsMatch(cardNumber, pattern, RegexOptions.None, TimeSpan.FromMilliseconds(100)))
            {
                return brand;
            }
        }

        return "Unknown";
    }

    private bool ValidateLuhn(string cardNumber)
    {
        int sum = 0;
        bool isSecondDigit = false;

        for (int i = cardNumber.Length - 1; i >= 0; i--)
        {
            int digit = cardNumber[i] - '0';

            if (isSecondDigit)
            {
                digit *= 2;
                if (digit > 9)
                    digit -= 9;
            }

            sum += digit;
            isSecondDigit = !isSecondDigit;
        }

        return sum % 10 == 0;
    }
}