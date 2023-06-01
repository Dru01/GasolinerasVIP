using System;
using System.Collections.Generic;
using System.Text;

namespace GasolinerasVIP.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public GasStation Station { get; set; }
        public double OrderedMagna { get; set; }
        public double OrderedPremium { get; set; }
        private DateTime _transactionTime;
        public DateTime TransactionTime
        {
            get => _transactionTime;
            set { _transactionTime = DateTime.Now; }
        }
        public int state { get; set; } // 1 = En la Estacion, 2 = En Camino, 3 = Pedido Realizado
    }
}
