using System.ComponentModel.DataAnnotations;

namespace WebAPI.Model
{
    public class CurrencyModel
    {
        [Required]
        public string Currency { get; set; }

        [Required]
        public string CurrencyName { get; set; }
    }
}
