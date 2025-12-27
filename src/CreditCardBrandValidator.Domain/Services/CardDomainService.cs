using CreditCardBrandValidator.Domain.Interfaces;
using System.Text.RegularExpressions;

namespace CreditCardBrandValidator.Domain.Services;

public class CardDomainService : ICardDomainService
{
    private static readonly Dictionary<string, string> CardPatterns = new()
    {
        { @"^4[0-9]{12}(?:[0-9]{3})?$", "Visa" },
        { @"^5[1-5][0-9]{14}$|^2(?:22[1-9]|2[3-9][0-9]|[3-6][0-9]{2}|7[01][0-9]|720)[0-9]{12}$", "Mastercard" },
        { @"^((457631|457632|438935|636368|636297|504175|5067|5090|627780|636297)[0-9]*)", "Elo" },
        { @"^3[47][0-9]{13}$", "American Express" },
        { @"^606282[0-9]{10}$|^3841[0-9]{15}$", "Hipercard" }
    };

    public (bool isValid, string brand) ValidateCard(string cardNumber)
    {
        if (string.IsNullOrWhiteSpace(cardNumber) || cardNumber.Length > 30)
            return (false, "Unknown");

        var cleanNumber = Regex.Replace(cardNumber, @"[^\d]", "");

        if (cleanNumber.Length < 12 || cleanNumber.Length > 19)
            return (false, "Unknown");

        var brand = GetCardBrand(cleanNumber);

        var isValid = ValidateLuhn(cleanNumber) && brand != "Unknown";

        return (isValid, brand);
    }

    private string GetCardBrand(string cardNumber)
    {
        foreach (var pattern in CardPatterns)
        {
            if (Regex.IsMatch(cardNumber, pattern.Key, RegexOptions.None, TimeSpan.FromMilliseconds(100)))
            {
                return pattern.Value;
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