using System;
using System.Collections.Generic;

namespace MS_ApiBurguer.Data.Models;

public partial class Promo
{
    public int PromoId { get; set; }

    public string? Description { get; set; }

    public DateTime FechaPromo { get; set; }

    public int BurgerId { get; set; }

    public virtual Burguer Burger { get; set; } = null!;
}
