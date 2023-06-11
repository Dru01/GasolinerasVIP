using System.ComponentModel.DataAnnotations;

namespace GasolinerasVIP.API.Models
{
    public class UserInfo
    {
        public string username { get; set; }
        [EmailAddress]
        public string email { get; set; }
        public string fullname { get; set; }
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}
