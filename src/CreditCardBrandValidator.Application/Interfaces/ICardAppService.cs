using CreditCardBrandValidator.Application.DTOs;

namespace CreditCardBrandValidator.Application.Interfaces;
public interface ICardAppService
{
    CardResponseDTO ExecuteCardValidation(CardRequestDTO request);
}
