namespace GasolinerasVIP.API.Models
{
    enum Status
    {
        RECEIVED, 
        IN_PROGRESS,
        COMPLETED
    }
    public class Transaction
    {
        public int Id { get; set; }
        // Needed UserID
        public GasStation GasStation { get; set; }
        public string Address { get; set; } 
        public DateTime ReceivedOrderDate { get; set; }
        public DateTime DeliveredOrderDate { get; set; }
        public int Status { get; set; } // ENUM {RECEIVED, IN_PROGRESS, COMPLETED}
        public int GasType { get; set; } // ENUM {MAGNA PREMIUM, DIESEL}
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
