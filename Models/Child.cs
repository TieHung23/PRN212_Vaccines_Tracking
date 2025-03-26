using System;
using System.Collections.Generic;

namespace Models;

public partial class Child
{
    public int Id { get; set; }

    public int ParentId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string Gender { get; set; } = null!;

    public int Status { get; set; }
}
