using System;
using System.Collections.Generic;

namespace ENDPOINTADMILER.Models;

public partial class MaintenanceReport
{
    public int ReportId { get; set; }

    public string? CustomerName { get; set; } //Nombre Cliente

    public string? Address { get; set; } //Direccion

    public string? PhoneNumber { get; set; } //Numero de telefono

    public string? SerialNumber { get; set; } //numeor de serie

    public string? LicensePlates { get; set; } //placas

    public string? Brand { get; set; } //marca

    public int? Mileage { get; set; } //kilometraje

    public string? FuelType { get; set; } //combustible

    public string? Model { get; set; }

    public string? Color { get; set; }

    public string? VisualDetails { get; set; } //detalles visuales

    public DateTime? EntryDate { get; set; } //fecha de ingreso

    public string? ValuableItems { get; set; } //objetos de valor

    public int? PkUser { get; set; }
}
