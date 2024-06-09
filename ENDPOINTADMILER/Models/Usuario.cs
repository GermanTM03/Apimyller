using System;
using System.Collections.Generic;

namespace ENDPOINTADMILER.Models;

public partial class Usuario
{
    public int Pkusuario { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoP { get; set; }

    public string? ApellidoM { get; set; }

    public string? Correo { get; set; }

    public string? Contra { get; set; }
}
