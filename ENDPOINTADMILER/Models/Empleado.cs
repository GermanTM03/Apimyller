using System;
using System.Collections.Generic;

namespace ENDPOINTADMILER.Models;

public partial class Empleado
{
    public int Pkempleado { get; set; }

    public string? NombreCompleto { get; set; }

    public int? Edad { get; set; }

    public string? CorreoEmpresarial { get; set; }

    public string? Contraseña { get; set; }

    public string? Rol { get; set; }

    public int? Fksucursal { get; set; }
}
