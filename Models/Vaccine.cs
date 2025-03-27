using System;
using System.Collections.Generic;

namespace Models;

public partial class Vaccine
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<VaccineDetail> VaccineDetails { get; set; } = new List<VaccineDetail>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
