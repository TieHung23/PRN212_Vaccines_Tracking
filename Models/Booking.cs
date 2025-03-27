using System;
using System.Collections.Generic;

namespace Models;

public partial class Booking
{
    public int Id { get; set; }

    public int ParentId { get; set; }

    public decimal FinalPrice { get; set; }

    public DateTime CreatedAt { get; set; }

    public int Status { get; set; }

    public virtual Customer Parent { get; set; } = null!;

    public virtual ICollection<VaccinesTracking> VaccinesTrackings { get; set; } = new List<VaccinesTracking>();

    public virtual ICollection<Child> Children { get; set; } = new List<Child>();

    public virtual ICollection<Vaccine> Vaccines { get; set; } = new List<Vaccine>();
}
