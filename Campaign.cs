using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Charity_Fundraising_DBMS;

public partial class Campaign
{
    public int CampaignId { get; set; }

    public string CTitle { get; set; } = null!;

    public string? CDescription { get; set; }

    public DateTime CStartDate { get; set; }

    public DateTime CEndDate { get; set; }

    public decimal CGoalAmount { get; set; }

    public string CStatus { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Donation>? Donations { get; set; } = new List<Donation>();

    [JsonIgnore]
    public virtual ICollection<FundDistribution>? FundDistributions { get; set; } = new List<FundDistribution>();
}
