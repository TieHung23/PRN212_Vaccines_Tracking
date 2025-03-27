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

    public int VaccineId { get; set; }

    public int Status { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual Child Child { get; set; } = null!;

    public virtual ICollection<VaccinesTracking> InversePrevious { get; set; } = new List<VaccinesTracking>();

    public virtual Customer Parent { get; set; } = null!;

    public virtual VaccinesTracking Previous { get; set; } = null!;

    public virtual Vaccine Vaccine { get; set; } = null!;
}
