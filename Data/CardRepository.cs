using RapidPayAPI.Entities;
using System.Linq;

namespace RapidPayAPI.Data
{
    public class CardRepository : EfCoreRepository<Card, RapidPayContext>
    {
        private readonly RapidPayContext _context;
        public CardRepository(RapidPayContext context) : base(context)
        {
            _context = context;
        }

        public Card FindByCardNumber(string cardNumber)
        {
            return _context.Set<Card>().FirstOrDefault(x => x.CardNumber == cardNumber);
        }
    }
}
