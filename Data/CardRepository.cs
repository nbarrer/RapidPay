using Microsoft.EntityFrameworkCore;
using RapidPayAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RapidPayAPI.Data
{
    public class CardRepository : EfCoreRepository<Card, RapidPayContext>
    {
        private readonly RapidPayContext _context;
        public CardRepository(RapidPayContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Card> FindByCardNumber(string cardnumber)
        {
            return await _context.Set<Card>().FirstOrDefaultAsync(x => x.CardNumber == cardnumber);
        }
    }
}
