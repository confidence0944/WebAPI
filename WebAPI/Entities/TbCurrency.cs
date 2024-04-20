using System;
using System.Collections.Generic;

namespace WebAPI.Entities;

public partial class TbCurrency
{
    public string Currency { get; set; } = null!;

    public string CurrencyName { get; set; } = null!;
}
