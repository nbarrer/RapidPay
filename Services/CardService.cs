using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RapidPayAPI.Data;
using RapidPayAPI.Entities;
using RapidPayAPI.Interfaces;
using RapidPayAPI.Models;
using System;
using System.Threading.Tasks;

namespace RapidPayAPI.Services
{
    public class CardService : ICardService
    {
        private readonly CardRepository _repository;
        private readonly ILogger<CardService> _logger;
        private readonly IConfiguration _configuration;
        public CardService(CardRepository repository, ILogger<CardService> logger, IConfiguration configuration)
        {
            _repository = repository;
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<Card> Create(CardModel newCard)
        {
            //Validates length format
            int cardFormatDigits = 0;
            int.TryParse(_configuration["AppSettings:CardFormatDigits"], out cardFormatDigits);

            if (newCard.CardNumber.Length != cardFormatDigits)
            {
                return null;
            }

            try
            {
                //The card number is already assign to a card
                if (await _repository.FindByCardNumber(newCard.CardNumber) != null)
                {
                    _logger.LogWarning($"The card {newCard.CardNumber} is already assign on system.");
                    return null;
                }

                Card newEntity = new Card
                {
                    Balance = newCard.Balance,
                    CardHolderName = newCard.CardHolderName,
                    CardNumber = newCard.CardNumber,
                    ExpirationDate = DateTime.Now.AddYears(10).ToString()
                };

                var newCardEntity = await _repository.Add(newEntity);
                _logger.LogInformation($"The card {newCard.CardNumber} was created on system.");

                return newCardEntity;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Creating card: {ex.Message}");
                return null;
            }
        }

        public async Task<Card> Delete(string cardNumber)
        {
            try
            {
                var card = await _repository.Find(x => x.CardNumber == cardNumber);
                var deletedCard = await _repository.Delete(card.Id);
                _logger.LogInformation($"The card {cardNumber} was deleted on system");

                return deletedCard;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Deleting Card on CardService: {ex.Message}");
                return null;
            }
        }

        public async Task<double?> GetBalance(string cardNumber)
        {
            try
            {
                var card = await _repository.Find(x => x.CardNumber == cardNumber);
                if (card == null)
                {
                    _logger.LogWarning($"The card {cardNumber} don't exists on system");
                    return null;
                }

                return card.Balance;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Getting Balance CardService: {ex.Message}");
                return null;
            }
        }

        public async Task<Card> Pay(string cardNumber, double amount)
        {
            try
            {
                var card = await _repository.FindByCardNumber(cardNumber);

                if(card == null)
                {
                    return null;
                }

                var total = card.Balance - amount;
                if (total < 0)
                {
                    _logger.LogWarning($"The Payment cannot be processed. Please check your balance.");
                    return null;
                }

                card.Balance = total;
                var updatedCard = await _repository.Update(card);
                _logger.LogInformation($"Payment to card: {cardNumber} with amount {amount}");

                return updatedCard;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on Payment: {ex.Message}");
                return null;
            };
        }
    }
}
