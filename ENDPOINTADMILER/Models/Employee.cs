using System;
using System.Collections.Generic;

namespace ENDPOINTADMILER.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? FullName { get; set; }

    public int? Age { get; set; }

    public string? CompanyEmail { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }

    public int? BranchId { get; set; }
}
