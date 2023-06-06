namespace GasolinerasVIP.API.Models
{
    enum GasType
    {
        MAGNA,
        PREMIUM,
        DIESEL
    }

    public class GasStation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public decimal Rating { get;set; }
        public decimal PriceMagna { get; set; }
        public decimal PricePremium { get; set; }
        public decimal PriceDiesel { get; set; }
        public string ProfileURL { get; set; }

        public string ProfileName { get; set; }
    }
}
