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
}
