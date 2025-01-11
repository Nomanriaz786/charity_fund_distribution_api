using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Charity_Fundraising_DBMS;

public partial class FundDistribution
{
    public int DistributionId { get; set; }
    public int CampaignId { get; set; }
    public string FBeneficiaryName { get; set; } = null!;
    public DateOnly FDistributionDate { get; set; }
    public decimal FAmountDistributed { get; set; }
    public string? FPurpose { get; set; }

    [JsonIgnore]
    public virtual Campaign? Campaign { get; set; }
}
