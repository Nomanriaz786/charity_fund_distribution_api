using Charity_Fundraising_DBMS;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

public static class API_Functions
{
    private static readonly CharityFundraisingDbmsContext db = new();

    public static IResult AddCampaign(Campaign campaign)
    {
        try
        {
            // Initialize collections if null to prevent JSON serialization issues
            campaign.Donations ??= new List<Donation>();
            
            db.Campaigns.Add(campaign);
            db.SaveChanges();
            return Results.Created($"/campaigns/{campaign.CampaignId}", campaign);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to add campaign: {ex.Message}");
        }
    }
    // display all campaigns
    public static IResult GetCampaigns()
    {
        var campaigns = db.Campaigns.ToList();
        return Results.Ok(campaigns);
    } 
    // update the campaign
     public static IResult UpdateCampaign(int campaignId, Campaign updatedCampaign)
    {
        try
        {
            // Ensure the CampaignId is set correctly
            if (campaignId != updatedCampaign.CampaignId)
                return Results.BadRequest("Campaign ID mismatch");

            // Retrieve the existing campaign without tracking it
            var existingCampaign = db.Campaigns.AsNoTracking().FirstOrDefault(c => c.CampaignId == campaignId);
            if (existingCampaign == null)
                return Results.NotFound("Campaign not found");

            // Attach the updated entity to the context
            db.Campaigns.Attach(updatedCampaign);
            db.Entry(updatedCampaign).State = EntityState.Modified;

            // Save changes to the database
            db.SaveChanges();
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to update campaign: {ex.Message}");
        }
    }
    // Delete the campaign
    public static IResult DeleteCampaign(int campaignId)
    {
        try
        {
            var campaign = db.Campaigns.Find(campaignId);
            if (campaign == null)
                return Results.NotFound("Campaign not found");

            db.Campaigns.Remove(campaign);
            db.SaveChanges();
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to delete campaign: {ex.Message}");
        }
    }
    // Add a donation
    public static IResult AddDonation(Donation donation)
    {
        try
        {
            if (donation == null)
                return Results.BadRequest("Invalid donation data");
            db.Donations.Add(donation);
            db.SaveChanges();
            return Results.Created($"/donations/{donation.DonationId}", donation);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to add donation: {ex.Message}");
        }
    }
    // Display all donations
    public static IResult GetDonations()
    {
        var donations = db.Donations.ToList();
        return Results.Ok(donations);
    }
    // Delete a donation
    public static IResult DeleteDonation(int donationId)
    {
        try
        {
            var donation = db.Donations.Find(donationId);
            if (donation == null)
                return Results.NotFound("Donation not found");

            db.Donations.Remove(donation);
            db.SaveChanges();
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to delete donation: {ex.Message}");
        }
    }
    // Add a donor
    public static IResult AddDonor(Donor donor)
    {
        try
        {
            if (donor == null)
                return Results.BadRequest("Invalid donor data");
            db.Donors.Add(donor);
            db.SaveChanges();
            return Results.Created($"/donors/{donor.DonorId}", donor);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to add donor: {ex.Message}");
        }
    }
    // Display all donors
    public static IResult GetDonors()
    {
        var donors = db.Donors.ToList();
        return Results.Ok(donors);
    }
    // Delete a donor
    public static IResult DeleteDonor(int donorId)
    {
        try
        {
            var donor = db.Donors.Find(donorId);
            if (donor == null)
                return Results.NotFound("Donor not found");

            db.Donors.Remove(donor);
            db.SaveChanges();
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to delete donor: {ex.Message}");
        }
    }
    // Add a fund distribution
    public static IResult AddFundDistribution(FundDistribution fundDistribution)
    {
        try
        {
            if (fundDistribution == null)
                return Results.BadRequest("Invalid fund distribution data");
            db.FundDistributions.Add(fundDistribution);
            db.SaveChanges();
            return Results.Created($"/funddistributions/{fundDistribution.DistributionId}", fundDistribution);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to add fund distribution: {ex.Message}");
        }
    }
    // Display all fund distributions
    public static IResult GetFundDistributions()
    {
        var distributions = db.FundDistributions.ToList();
        return Results.Ok(distributions);
    }
    // Delete a fund distribution
    public static IResult DeleteFundDistribution(int distributionId)
    {
        try
        {
            var fundDistribution = db.FundDistributions.Find(distributionId);
            if (fundDistribution == null)
                return Results.NotFound("Fund distribution not found");

            db.FundDistributions.Remove(fundDistribution);
            db.SaveChanges();
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to delete fund distribution: {ex.Message}");
        }
    }
    
    // update the donation
    public static IResult UpdateDonation(int donationId, Donation updatedDonation)
    {
        try
        {
            // Ensure the DonationId is set correctly
            if (donationId != updatedDonation.DonationId)
                return Results.BadRequest("Donation ID mismatch");

            // Retrieve the existing donation without tracking it
            var existingDonation = db.Donations.AsNoTracking().FirstOrDefault(d => d.DonationId == donationId);
            if (existingDonation == null)
                return Results.NotFound("Donation not found");

            // Attach the updated entity to the context
            db.Donations.Attach(updatedDonation);
            db.Entry(updatedDonation).State = EntityState.Modified;

            // Save changes to the database
            db.SaveChanges();
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to update donation: {ex.Message}");
        }
    }
    // update the donor
    public static IResult UpdateDonor(int donorId, Donor updatedDonor)
    {
        try
        {
            // Ensure the DonorId is set correctly
            if (donorId != updatedDonor.DonorId)
                return Results.BadRequest("Donor ID mismatch");

            // Retrieve the existing donor without tracking it
            var existingDonor = db.Donors.AsNoTracking().FirstOrDefault(d => d.DonorId == donorId);
            if (existingDonor == null)
                return Results.NotFound("Donor not found");

            // Attach the updated entity to the context
            db.Donors.Attach(updatedDonor);
            db.Entry(updatedDonor).State = EntityState.Modified;

            // Save changes to the database
            db.SaveChanges();
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to update donor: {ex.Message}");
        }
    }
    // update the fund distribution
    public static IResult UpdateFundDistribution(int distributionId, FundDistribution updatedFundDistribution)
    {
        try
        {
            // Ensure the DistributionId is set correctly
            if (distributionId != updatedFundDistribution.DistributionId)
                return Results.BadRequest("Distribution ID mismatch");

            // Retrieve the existing fund distribution without tracking it
            var existingFundDistribution = db.FundDistributions.AsNoTracking().FirstOrDefault(fd => fd.DistributionId == distributionId);
            if (existingFundDistribution == null)
                return Results.NotFound("Fund distribution not found");

            // Attach the updated entity to the context
            db.FundDistributions.Attach(updatedFundDistribution);
            db.Entry(updatedFundDistribution).State = EntityState.Modified;

            // Save changes to the database
            db.SaveChanges();
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to update fund distribution: {ex.Message}");
        }
    }
    // Get a campaign by ID
    public static IResult GetCampaignById(int campaignId)
    {
        var campaign = db.Campaigns.Find(campaignId);
        return campaign == null ? Results.NotFound("Campaign not found") : Results.Ok(campaign);
    }
    // Get a donation by ID
    public static IResult GetDonationById(int donationId)
    {
        var donation = db.Donations.Find(donationId);
        return donation == null ? Results.NotFound("Donation not found") : Results.Ok(donation);
    }
    // Get a donor by ID
    public static IResult GetDonorById(int donorId)
    {
        var donor = db.Donors.Find(donorId);
        return donor == null ? Results.NotFound("Donor not found") : Results.Ok(donor);
    }
    // Get a fund distribution by ID
    public static IResult GetFundDistributionById(int distributionId)
    {
        var distribution = db.FundDistributions.Find(distributionId);
        return distribution == null ? Results.NotFound("Fund distribution not found") : Results.Ok(distribution);
    }
    // Get all donations for a campaign
    public static IResult GetDonationsForCampaign(int campaignId)
    {
        var donations = db.Donations.Where(d => d.CampaignId == campaignId).ToList();
        return donations.Any() ? Results.Ok(donations) : Results.NotFound("No donations found for this campaign");
    }
    // Get all fund distributions for a campaign
    public static IResult GetFundDistributionsForCampaign(int campaignId)
    {
        var distributions = db.FundDistributions.Where(f => f.CampaignId == campaignId).ToList();
        return distributions.Any() ? Results.Ok(distributions) : Results.NotFound("No distributions found for this campaign");
    }
    // Get all donations for a donor
    public static IResult GetDonationsForDonor(int donorId)
    {
        var donations = db.Donations.Where(d => d.DonorId == donorId).ToList();
        return donations.Any() ? Results.Ok(donations) : Results.NotFound("No donations found for this donor");
    }

    // Get all fund distributions for a beneficiary
    public static IResult GetFundDistributionsForBeneficiary(string beneficiaryName)
    {
        var distributions = db.FundDistributions.Where(f => f.FBeneficiaryName == beneficiaryName).ToList();
        return distributions.Any() ? Results.Ok(distributions) : Results.NotFound("No distributions found for this beneficiary");
    }

    // Get all fund distributions for a campaign and beneficiary
    public static IResult GetFundDistributionsForCampaignAndBeneficiary(int campaignId, string beneficiaryName)
    {
        var distributions = db.FundDistributions
            .Where(f => f.CampaignId == campaignId && f.FBeneficiaryName == beneficiaryName)
            .ToList();
        return distributions.Any() ? Results.Ok(distributions) : Results.NotFound("No distributions found");
    }

    // Get the total amount donated for a campaign
    public static IResult GetTotalDonationsForCampaign(int campaignId)
    {
        var total = db.Donations.Where(d => d.CampaignId == campaignId).Sum(d => d.DAmount);
        return Results.Ok(total);
    }

    // Get the total amount distributed for a campaign
    public static IResult GetTotalDistributionsForCampaign(int campaignId)
    {
        var total = db.FundDistributions.Where(f => f.CampaignId == campaignId).Sum(f => f.FAmountDistributed);
        return Results.Ok(total);
    }

    // Get the total amount donated by a donor
    public static IResult GetTotalDonationsForDonor(int donorId)
    {
        var total = db.Donations.Where(d => d.DonorId == donorId).Sum(d => d.DAmount);
        return Results.Ok(total);
    }

    // Get the total amount distributed to a beneficiary
    public static IResult GetTotalDistributionsForBeneficiary(string beneficiaryName)
    {
        var total = db.FundDistributions.Where(f => f.FBeneficiaryName == beneficiaryName).Sum(f => f.FAmountDistributed);
        return Results.Ok(total);
    }

    // Get the total amount distributed to a beneficiary for a campaign
    public static IResult GetTotalDistributionsForCampaignAndBeneficiary(int campaignId, string beneficiaryName)
    {
        var total = db.FundDistributions
            .Where(f => f.CampaignId == campaignId && f.FBeneficiaryName == beneficiaryName)
            .Sum(f => f.FAmountDistributed);
        return Results.Ok(total);
    }

    // Get the total amount donated for a campaign by a donor
    public static IResult GetTotalDonationsForCampaignAndDonor(int campaignId, int donorId)
    {
        var total = db.Donations
            .Where(d => d.CampaignId == campaignId && d.DonorId == donorId)
            .Sum(d => d.DAmount);
        return Results.Ok(total);
    }
}
