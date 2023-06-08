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
        public decimal MagnaPrice { get; set; }
        public decimal PremiumPrice { get; set; }
        public decimal DieselPrice { get; set; }
        public string ImageUrl { get; set; }
    }
}
