using Charity_Fundraising_DBMS;

public class API_Functions
{
    private readonly CharityFundraisingDbmsContext db = new();

    public record OperationResult(bool Success, string Message = "");

    public OperationResult AddCampaign(Campaign campaign)
    {
        try
        {
            // Initialize collections if null to prevent JSON serialization issues
            campaign.Donations ??= new List<Donation>();
            
            db.Campaigns.Add(campaign);
            db.SaveChanges();
            return new OperationResult(true);
        }
        catch (Exception ex)
        {
            return new OperationResult(false, $"Failed to add campaign: {ex.Message}");
        }
    }
    // display all campaigns
    public List<Campaign> GetCampaigns() => db.Campaigns.ToList();
    // Delete the campaign
    public OperationResult DeleteCampaign(int campaignId)
    {
        try
        {
            var campaign = db.Campaigns.Find(campaignId);
            if (campaign == null)
                return new OperationResult(false, "Campaign not found");

            db.Campaigns.Remove(campaign);
            db.SaveChanges();
            return new OperationResult(true);
        }
        catch (Exception ex)
        {
            return new OperationResult(false, $"Failed to delete campaign: {ex.Message}");
        }
    }
    // Add a donation
    public OperationResult AddDonation(Donation donation)
    {
        try
        {
            db.Donations.Add(donation);
            db.SaveChanges();
            return new OperationResult(true);
        }
        catch (Exception ex)
        {
            return new OperationResult(false, $"Failed to add donation: {ex.Message}");
        }
    }
    // Display all donations
    public List<Donation> GetDonations() => db.Donations.ToList();
    // Delete a donation
    public OperationResult DeleteDonation(int donationId)
    {
        try
        {
            var donation = db.Donations.Find(donationId);
            if (donation == null)
                return new OperationResult(false, "Donation not found");

            db.Donations.Remove(donation);
            db.SaveChanges();
            return new OperationResult(true);
        }
        catch (Exception ex)
        {
            return new OperationResult(false, $"Failed to delete donation: {ex.Message}");
        }
    }
    // Add a donor
    public OperationResult AddDonor(Donor donor)
    {
        try
        {
            db.Donors.Add(donor);
            db.SaveChanges();
            return new OperationResult(true);
        }
        catch (Exception ex)
        {
            return new OperationResult(false, $"Failed to add donor: {ex.Message}");
        }
    }
    // Display all donors
    public List<Donor> GetDonors() => db.Donors.ToList();
    // Delete a donor
    public OperationResult DeleteDonor(int donorId)
    {
        try
        {
            var donor = db.Donors.Find(donorId);
            if (donor == null)
                return new OperationResult(false, "Donor not found");

            db.Donors.Remove(donor);
            db.SaveChanges();
            return new OperationResult(true);
        }
        catch (Exception ex)
        {
            return new OperationResult(false, $"Failed to delete donor: {ex.Message}");
        }
    }
    // Add a fund distribution
    public OperationResult AddFundDistribution(FundDistribution fundDistribution)
    {
        try
        {
            db.FundDistributions.Add(fundDistribution);
            db.SaveChanges();
            return new OperationResult(true);
        }
        catch (Exception ex)
        {
            return new OperationResult(false, $"Failed to add fund distribution: {ex.Message}");
        }
    }
    // Display all fund distributions
    public List<FundDistribution> GetFundDistributions() => db.FundDistributions.ToList();
    // Delete a fund distribution
    public OperationResult DeleteFundDistribution(int distributionId)
    {
        try
        {
            var fundDistribution = db.FundDistributions.Find(distributionId);
            if (fundDistribution == null)
                return new OperationResult(false, "Fund distribution not found");

            db.FundDistributions.Remove(fundDistribution);
            db.SaveChanges();
            return new OperationResult(true);
        }
        catch (Exception ex)
        {
            return new OperationResult(false, $"Failed to delete fund distribution: {ex.Message}");
        }
    }
    // update the campaign
    public OperationResult UpdateCampaign(Campaign campaign)
    {
        try
        {
            db.Campaigns.Update(campaign);
            db.SaveChanges();
            return new OperationResult(true);
        }
        catch (Exception ex)
        {
            return new OperationResult(false, $"Failed to update campaign: {ex.Message}");
        }
    }
    // update the donation
    public OperationResult UpdateDonation(Donation donation)
    {
        try
        {
            db.Donations.Update(donation);
            db.SaveChanges();
            return new OperationResult(true);
        }
        catch (Exception ex)
        {
            return new OperationResult(false, $"Failed to update donation: {ex.Message}");
        }
    }
    // update the donor
    public OperationResult UpdateDonor(Donor donor)
    {
        try
        {
            db.Donors.Update(donor);
            db.SaveChanges();
            return new OperationResult(true);
        }
        catch (Exception ex)
        {
            return new OperationResult(false, $"Failed to update donor: {ex.Message}");
        }
    }
    // update the fund distribution
    public OperationResult UpdateFundDistribution(FundDistribution fundDistribution)
    {
        try
        {
            db.FundDistributions.Update(fundDistribution);
            db.SaveChanges();
            return new OperationResult(true);
        }
        catch (Exception ex)
        {
            return new OperationResult(false, $"Failed to update fund distribution: {ex.Message}");
        }
    }
    // Get a campaign by ID
    public Campaign GetCampaignById(int campaignId) => db.Campaigns.Find(campaignId) ?? new Campaign();
    // Get a donation by ID
    public Donation GetDonationById(int donationId) => db.Donations.Find(donationId)!;
    // Get a donor by ID
    public Donor GetDonorById(int donorId) => db.Donors.Find(donorId)!;
    // Get a fund distribution by ID
    public FundDistribution GetFundDistributionById(int distributionId) => db.FundDistributions.Find(distributionId) ?? new FundDistribution();
    // Get all donations for a campaign
    public List<Donation> GetDonationsForCampaign(int campaignId) => db.Donations.Where(d => d.CampaignId == campaignId).ToList();
    // Get all fund distributions for a campaign
    public List<FundDistribution> GetFundDistributionsForCampaign(int campaignId) => db.FundDistributions.Where(f => f.CampaignId == campaignId).ToList();
    // Get all donations for a donor
    public List<Donation> GetDonationsForDonor(int donorId) => db.Donations.Where(d => d.DonorId == donorId).ToList();
    // Get all fund distributions for a beneficiary
    public List<FundDistribution> GetFundDistributionsForBeneficiary(string beneficiaryName) => db.FundDistributions.Where(f => f.FBeneficiaryName == beneficiaryName).ToList();
    // Get all fund distributions for a campaign and beneficiary
    public List<FundDistribution> GetFundDistributionsForCampaignAndBeneficiary(int campaignId, string beneficiaryName) => db.FundDistributions.Where(f => f.CampaignId == campaignId && f.FBeneficiaryName == beneficiaryName).ToList();
    // Get the total amount donated for a campaign
    public decimal GetTotalDonationsForCampaign(int campaignId) => db.Donations.Where(d => d.CampaignId == campaignId).Sum(d => d.DAmount);
    // Get the total amount distributed for a campaign
    public decimal GetTotalDistributionsForCampaign(int campaignId) => db.FundDistributions.Where(f => f.CampaignId == campaignId).Sum(f => f.FAmountDistributed);
    // Get the total amount donated by a donor
    public decimal GetTotalDonationsForDonor(int donorId) => db.Donations.Where(d => d.DonorId == donorId).Sum(d => d.DAmount);
    // Get the total amount distributed to a beneficiary
    public decimal GetTotalDistributionsForBeneficiary(string beneficiaryName) => db.FundDistributions.Where(f => f.FBeneficiaryName == beneficiaryName).Sum(f => f.FAmountDistributed);
    // Get the total amount distributed to a beneficiary for a campaign
    public decimal GetTotalDistributionsForCampaignAndBeneficiary(int campaignId, string beneficiaryName) => db.FundDistributions.Where(f => f.CampaignId == campaignId && f.FBeneficiaryName == beneficiaryName).Sum(f => f.FAmountDistributed);
    // Get the total amount donated for a campaign by a donor
    public decimal GetTotalDonationsForCampaignAndDonor(int campaignId, int donorId) => db.Donations.Where(d => d.CampaignId == campaignId && d.DonorId == donorId).Sum(d => d.DAmount);
    
    
}
