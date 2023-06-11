using System.ComponentModel.DataAnnotations;

namespace GasolinerasVIP.Models
{
    public class UserRegistry
    {
        public string username { get; set; }
        [EmailAddress]
        public string email { get; set; }
        public string fullname { get; set; }
        public string password { get; set; }
    }
}
