using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Charity_Fundraising_DBMS;

public partial class Donor
{
    public int DonorId { get; set; }

    public string DName { get; set; } = null!;

    public string? DEmail { get; set; }

    public string? DPhone { get; set; }

    public string? DAddress { get; set; }
    [JsonIgnore]
    public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();
}
