using System;
using System.Collections.Generic;

namespace Models;

public partial class VaccinesTracking
{
    public int Id { get; set; }

    public int PreviousId { get; set; }

    public DateTime DateVaccination { get; set; }

    public int ChildId { get; set; }

    public int ParentId { get; set; }

    public int BookingId { get; set; }
}
