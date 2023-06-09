using GasolinerasVIP.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;

namespace GasolinerasVIP.Models
{
    public class Token
    {
        public string Access_Token { get; set; }
        public string Refresh_Token { get; set; }
    }
}
