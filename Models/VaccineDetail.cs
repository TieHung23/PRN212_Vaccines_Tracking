using System;
using System.Collections.Generic;

namespace Models;

public partial class VaccineDetail
{
    public int VaccineDetailsId { get; set; }

    public int VaccineId { get; set; }

    public DateTime EntryDate { get; set; }

    public long Quantity { get; set; }

    public int Status { get; set; }
}
