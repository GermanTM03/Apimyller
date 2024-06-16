using System;
using System.Collections.Generic;

namespace ENDPOINTADMILER.Models;

public partial class User
{
    public int PkUser { get; set; }

    public string? FullName { get; set; }


    public string? Email { get; set; }

    public string? Password { get; set; }
}
