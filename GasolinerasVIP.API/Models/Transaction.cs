using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey("GasStation")]
        public int GasStationId { get; set; }
        public virtual GasStation GasStation { get; set; }
        public string Address { get; set; } 
        public DateTime ReceivedOrderDate { get; set; }
        public DateTime DeliveredOrderDate { get; set; }
        public int Status { get; set; } // ENUM {RECEIVED, IN_PROGRESS, COMPLETED}
        public decimal MagnaPrice { get; set; }
        public decimal OrderedMagna { get; set; }
        public decimal PremiumPrice { get; set; }
        public decimal OrderedPremium { get; set; }
        public decimal DieselPrice { get; set; }
        public decimal OrderedDiesel { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal ServiceFee { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal Disccount { get; set; }
        public decimal Total { get; set; }
        // Needed Payment method
    }
}
