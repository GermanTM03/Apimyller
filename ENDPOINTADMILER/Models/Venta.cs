using System;
using System.Collections.Generic;

namespace ENDPOINTADMILER.Models;

public partial class Venta
{
    public int Pkventa { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Vendedor { get; set; }

    public string? Producto { get; set; }

    public decimal? Valor { get; set; }

    public string? Vienda { get; set; }

    public int? Fksucursal { get; set; }
}
