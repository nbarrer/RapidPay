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
            return Ok(card);
        }

        [HttpGet("getbalance")]
        public async Task<IActionResult> GetBalance(string cardNumber)
        {
            var balance = await _cardService.GetBalance(cardNumber);
            if (balance == null)
            {
                return StatusCode(501, "Error getting balance");
            }

            return Ok(balance);
        }

        [HttpGet("paycard")]
        public async Task<IActionResult> Pay(string cardNumber, double amount)
        {
            var fee = _paymeentFeeService.CalculateFee();
            var total = amount + (amount * fee);
            var card = await _cardService.Pay(cardNumber, total);

            if(card == null)
            {
                return StatusCode(500);
            }

            return Ok(card);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string cardNumber)
        {
            var card = await _cardService.Delete(cardNumber);

            if (card == null)
            {
                return NotFound(card);
            }

            return Ok(card);
        }
    }
}
