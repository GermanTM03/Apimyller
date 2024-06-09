using System;
using System.Collections.Generic;

namespace ENDPOINTADMILER.Models;

public partial class Sucursal
{
    public int Pksucursal { get; set; }

    public string? NombreNegocio { get; set; }

    public string? Direccion { get; set; }

    public string? Rfc { get; set; }

    public string? CorreoS { get; set; }

    public string? NumeroTelefono { get; set; }

    public int? Fkusuario { get; set; }
}
