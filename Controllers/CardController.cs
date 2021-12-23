using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPayAPI.Interfaces;
using RapidPayAPI.Models;
using System.Threading.Tasks;

namespace RappidPay.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CardController : ControllerBase
    {
        private ICardService _cardService;
        private IPaymentFeeService _paymeentFeeService;

        public CardController(ICardService cardService, IPaymentFeeService paymeentFeeService)
        {
            _cardService = cardService;
            _paymeentFeeService = paymeentFeeService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCard([FromBody] CardModel model)
        {
            var card = await _cardService.Create(model);
            
            if(card == null)
            {
                return StatusCode(500);
            }
            return Ok(201);
        }

        [HttpGet("getbalance")]
        public IActionResult GetBalance(string cardNumber)
        {
            var balance = _cardService.GetBalance(cardNumber);
            if (balance == null)
            {
                return StatusCode(501, "Error getting balance");
            }

            return Ok(balance);
        }

        [HttpGet("paycard")]
        public IActionResult Pay(string cardNumber, double amount)
        {
            var fee = _paymeentFeeService.CalculateFee();
            var total = amount + (amount * fee);
            var card = _cardService.Pay(cardNumber, total);

            if(card == null)
            {
                return StatusCode(500);
            }

            return Ok(card);
        }
    }
}
