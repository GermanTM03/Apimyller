using System;
using System.Collections.Generic;

namespace ENDPOINTADMILER.Models;

public partial class ReporteMantenimiento
{
    public int Pkreporte { get; set; }

    public int? Folio { get; set; }

    public string? NombreCliente { get; set; }

    public string? Direccion { get; set; }

    public string? NumeroTelefono { get; set; }

    public string? NumeroDeSerie { get; set; }

    public string? Placas { get; set; }

    public string? Marca { get; set; }

    public int? Kilometraje { get; set; }

    public string? Combustible { get; set; }

    public string? Modelo { get; set; }

    public string? Color { get; set; }

    public string? DetallesVisuales { get; set; }

    public DateTime? FechaIngreso { get; set; }

    public string? ObjetosDeValor { get; set; }

    public int? Fkusuario { get; set; }
}
