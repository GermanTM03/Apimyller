using System;
using System.Collections.Generic;

namespace ENDPOINTADMILER.Models;

public partial class Cita
{
    public int Pkcita { get; set; }

    public string? Persona { get; set; }

    public string? Razon { get; set; }

    public DateOnly? Fecha { get; set; }

    public TimeOnly? Hora { get; set; }

    public int? Fksucursal { get; set; }
}
