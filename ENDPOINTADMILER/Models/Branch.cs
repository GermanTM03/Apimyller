using System;
using System.Collections.Generic;

namespace ENDPOINTADMILER.Models;

public partial class Branch
{
    public int BranchId { get; set; }

    public string? BusinessName { get; set; } //Nombre negocio

    public string? Address { get; set; }//Direccion

    public string? RFC { get; set; } //RFC

    public string? Email { get; set; } //correo

    public string? PhoneNumber { get; set; } //numero de telefono

    public int? PkUser { get; set; }
}
