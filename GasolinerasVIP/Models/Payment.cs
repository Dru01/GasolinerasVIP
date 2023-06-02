using System;
using System.Collections.Generic;
using System.Text;

namespace GasolinerasVIP.Models
{
    internal class Payment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Int64 CardNumber { get; set; }
        public string CardNickName { get; set; }
        public string NameOnCard { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string ShortED
        {
            get { return ExpirationDate.ToString("MM/yy"); }
        }
    }
}
