using System;
using System.Collections.Generic;

namespace ENDPOINTADMILER.Models;

public partial class Appointment
{
    public int Pkappointment { get; set; }

    public string? Person { get; set; }

    public string? Reason { get; set; }

    public DateOnly? Date { get; set; }

    public TimeOnly? Hour { get; set; }

    public int? BranchId { get; set; }
}
