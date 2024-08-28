using System;
using System.Collections.Generic;

namespace ADO.NET_HW15.Models;

public partial class List
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Color { get; set; } = null!;

    public int? CaloricContent { get; set; }
}
