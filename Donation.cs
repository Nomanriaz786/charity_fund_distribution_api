using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Charity_Fundraising_DBMS;

public partial class Donation
{
    public int DonationId { get; set; }

    public int DonorId { get; set; }

    public int CampaignId { get; set; }

    public DateOnly DDonationDate { get; set; }

    public decimal DAmount { get; set; }

    public string? DPaymentMethod { get; set; }

    [JsonIgnore]
    public virtual Campaign? Campaign { get; set; }

    [JsonIgnore]
    public virtual Donor? Donor { get; set; }
}
