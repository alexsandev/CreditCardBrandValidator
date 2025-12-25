namespace CreditCardBrandValidator.Domain.Interfaces;

public interface ICardDomainService
{
    (bool isValid, string brand) ValidateCard(string cardNumber);
}