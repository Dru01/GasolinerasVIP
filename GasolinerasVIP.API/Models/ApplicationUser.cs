using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GasolinerasVIP.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? Address { get; set; }

    }
}
