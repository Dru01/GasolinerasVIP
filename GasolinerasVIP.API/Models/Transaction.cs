namespace GasolinerasVIP.API.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        // Needed GasStation
        // Needed UserID
        // Needed GasPipe?
        // Needed Address

        public DateTime ReceivedOrderDate { get; set; }
        public DateTime DeliveredOrderDate { get; set; }
        public int Status { get; set; } // ENUM? Recibido, en camino, completado
        public int GasType { get; set; } // ENUM? Magna, Premium, Diesel
        public decimal GasPrice { get; set; }
        public decimal Liters { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal ServiceFee { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal Disccount { get; set; }
        public decimal Total { get; set; }

        // Needed Payment method
    }
}
