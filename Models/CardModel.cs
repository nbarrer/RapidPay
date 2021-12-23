using System.ComponentModel.DataAnnotations;

namespace RapidPayAPI.Models
{
    public class CardModel
    {
        [Required]
        public string CardHolderName { get; set; }

        [Required]
        public double Balance { get; set; }

        [Required]
        public string CardNumber { get; set; }

    }
}
