using System;

namespace GasolinerasVIP.Models
{
    public class UserToken
    {
        public string token { get; set; }
        public DateTime expires { get; set; }
    }
}
