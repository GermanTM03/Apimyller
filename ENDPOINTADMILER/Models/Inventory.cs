using System;
using System.Collections.Generic;

namespace ENDPOINTADMILER.Models;

public partial class Inventory
{
    public int ProductId { get; set; }//idproducto

    public string? Brand { get; set; }//marca

    public string? ProductName { get; set; }//nombre producto

    public string? Description { get; set; }//descripcion

    public int? Quantity { get; set; }//cantidad

    public decimal? Cost { get; set; }//Costo

    public int? BranchId { get; set; } //Fk sucursal

}
