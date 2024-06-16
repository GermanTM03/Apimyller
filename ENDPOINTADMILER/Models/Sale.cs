using System;
using System.Collections.Generic;

namespace ENDPOINTADMILER.Models;

public partial class Sale
{
    public int SaleId { get; set; }

    public DateTime? Date { get; set; }

    public string? Seller { get; set; } //vendedor

    public string? Product { get; set; } //producto

    public decimal? cost { get; set; } //costo

    public int? BranchId { get; set; } //sucursal
}
