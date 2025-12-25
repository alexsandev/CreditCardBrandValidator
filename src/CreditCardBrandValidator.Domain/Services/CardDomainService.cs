using CreditCardBrandValidator.Domain.Interfaces;

namespace CreditCardBrandValidator.Domain.Services;

public class CardDomainService : ICardDomainService
{
    public (bool isValid, string brand) ValidateCard(string cardNumber)
    {
        throw new NotImplementedException();
    }

    //metodo para validar bandeira
    
    //algoritimo de Luhn
}