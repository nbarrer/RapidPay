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
                if (_repository.FindByCardNumber(newCard.CardNumber) != null)
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

        public bool Delete(string cardNumber)
        {
            throw new NotImplementedException();
        }

        public double? GetBalance(string cardNumber)
        {
            if (_repository.FindByCardNumber(cardNumber) == null)
            {
                _logger.LogWarning($"The card {cardNumber} don't exists on system");
                return null;
            }

            try
            {
                return _repository.FindByCardNumber(cardNumber).Balance;
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
                Card card = _repository.FindByCardNumber(cardNumber);
                card.Balance = card.Balance - amount;
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
