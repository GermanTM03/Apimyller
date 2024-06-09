using System;
using System.Collections.Generic;

namespace ENDPOINTADMILER.Models;

public partial class Inventario
{
    public int Pkproducto { get; set; }

    public string? Marca { get; set; }

    public string? NombreProducto { get; set; }

    public string? Descripcion { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Costo { get; set; }

    public int? Fksucursal { get; set; }
}
