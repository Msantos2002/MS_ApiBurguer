using System;
using System.Collections.Generic;

namespace MS_ApiBurguer.Data.Models;

public partial class Burguer
{
    public int BurguerId { get; set; }

    public string Name { get; set; } = null!;

    public bool WithCheese { get; set; }

    public decimal Price { get; set; }

    public decimal PriceCelling { get; set; }

    public virtual ICollection<Promo> Promos { get; set; } = new List<Promo>();
}
