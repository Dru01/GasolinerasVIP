using System.ComponentModel.DataAnnotations;

namespace GasolinerasVIP.API.Models
{
    public class UserInfo
    {
        public string username { get; set; }
        [EmailAddress]
        public string email { get; set; }
        public string password { get; set; }
    }
}
