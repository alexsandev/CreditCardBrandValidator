using CreditCardBrandValidator.Application.DTOs;
using CreditCardBrandValidator.Application.Interfaces;
using CreditCardBrandValidator.Domain.Interfaces;

namespace CreditCardBrandValidator.Application.Services;

public class CardAppService : ICardAppService
{
    private readonly ICardDomainService _cardDomainService;

    public CardAppService(ICardDomainService cardDomainService)
    {
        _cardDomainService = cardDomainService;
    }

    public CardResponseDTO ExecuteCardValidation(CardRequestDTO request)
    {
        var (isValid, brand) = _cardDomainService.ValidateCard(request.CardNumber);

        return new CardResponseDTO(isValid, brand);
    }
}