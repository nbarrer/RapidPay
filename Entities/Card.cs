namespace RapidPayAPI.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public double Balance { get; set; }
        public string CardHolderName {get; set; }

    }
}
