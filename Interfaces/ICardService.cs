using RapidPayAPI.Entities;
using RapidPayAPI.Models;
using System.Threading.Tasks;

namespace RapidPayAPI.Interfaces
{
    public interface ICardService
    {
        Task<Card> Create(CardModel newCard);
        Task<Card> Pay(string cardNumber, double amount);
        bool Delete(string cardNumber);
        double? GetBalance(string cardNumber);
    }
}
