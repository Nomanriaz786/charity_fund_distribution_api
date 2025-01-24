using Charity_Fundraising_DBMS;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

public static class API_Functions
{
    private static IServiceProvider? ServiceProvider { get; set; }

    public static void Initialize(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    private static CharityFundraisingDbmsContext GetContext()
    {
        if (ServiceProvider == null)
        {
            throw new InvalidOperationException("ServiceProvider is not initialized.");
        }
        var scope = ServiceProvider.CreateScope();
        return scope.ServiceProvider.GetRequiredService<CharityFundraisingDbmsContext>();
    }

    public static IResult AddCampaign(Campaign campaign)
    {
        try
        {
            using var context = GetContext();
            campaign.Donations ??= new List<Donation>();
            context.Campaigns.Add(campaign);
            context.SaveChanges();
            return Results.Created($"/campaigns/{campaign.CampaignId}", campaign);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to add campaign: {ex.Message}");
        }
    }

    public static IResult GetCampaigns()
    {
        try
        {
            using var context = GetContext();
            var campaigns = context.Campaigns.ToList();
            return Results.Ok(campaigns);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to get campaigns: {ex.Message}");
        }
    }

    public static IResult UpdateCampaign(int campaignId, Campaign updatedCampaign)
    {
        try
        {
            using var context = GetContext();
            if (campaignId != updatedCampaign.CampaignId)
                return Results.BadRequest("Campaign ID mismatch");

            var existingCampaign = context.Campaigns.AsNoTracking().FirstOrDefault(c => c.CampaignId == campaignId);
            if (existingCampaign == null)
                return Results.NotFound("Campaign not found");

            context.Campaigns.Attach(updatedCampaign);
            context.Entry(updatedCampaign).State = EntityState.Modified;
            context.SaveChanges();
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to update campaign: {ex.Message}");
        }
    }

    public static IResult DeleteCampaign(int campaignId)
    {
        try
        {
            using var context = GetContext();
            var campaign = context.Campaigns.Find(campaignId);
            if (campaign == null)
                return Results.NotFound("Campaign not found");

            context.Campaigns.Remove(campaign);
            context.SaveChanges();
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to delete campaign: {ex.Message}");
        }
    }

    public static IResult AddDonation(Donation donation)
    {
        try
        {
            using var context = GetContext();
            if (donation == null)
                return Results.BadRequest("Invalid donation data");
            context.Donations.Add(donation);
            context.SaveChanges();
            return Results.Created($"/donations/{donation.DonationId}", donation);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to add donation: {ex.Message}");
        }
    }

    public static IResult GetDonations()
    {
        try
        {
            using var context = GetContext();
            var donations = context.Donations.ToList();
            return Results.Ok(donations);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to get donations: {ex.Message}");
        }
    }

    public static IResult DeleteDonation(int donationId)
    {
        try
        {
            using var context = GetContext();
            var donation = context.Donations.Find(donationId);
            if (donation == null)
                return Results.NotFound("Donation not found");

            context.Donations.Remove(donation);
            context.SaveChanges();
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to delete donation: {ex.Message}");
        }
    }

    public static IResult AddDonor(Donor donor)
    {
        try
        {
            using var context = GetContext();
            if (donor == null)
                return Results.BadRequest("Invalid donor data");
            context.Donors.Add(donor);
            context.SaveChanges();
            return Results.Created($"/donors/{donor.DonorId}", donor);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to add donor: {ex.Message}");
        }
    }

    public static IResult GetDonors()
    {
        try
        {
            using var context = GetContext();
            var donors = context.Donors.ToList();
            return Results.Ok(donors);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to get donors: {ex.Message}");
        }
    }

    public static IResult DeleteDonor(int donorId)
    {
        try
        {
            using var context = GetContext();
            var donor = context.Donors.Find(donorId);
            if (donor == null)
                return Results.NotFound("Donor not found");

            context.Donors.Remove(donor);
            context.SaveChanges();
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to delete donor: {ex.Message}");
        }
    }

    public static IResult AddFundDistribution(FundDistribution fundDistribution)
    {
        try
        {
            using var context = GetContext();
            if (fundDistribution == null)
                return Results.BadRequest("Invalid fund distribution data");
            context.FundDistributions.Add(fundDistribution);
            context.SaveChanges();
            return Results.Created($"/funddistributions/{fundDistribution.DistributionId}", fundDistribution);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to add fund distribution: {ex.Message}");
        }
    }

    public static IResult GetFundDistributions()
    {
        try
        {
            using var context = GetContext();
            var distributions = context.FundDistributions.ToList();
            return Results.Ok(distributions);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to get fund distributions: {ex.Message}");
        }
    }

    public static IResult DeleteFundDistribution(int distributionId)
    {
        try
        {
            using var context = GetContext();
            var fundDistribution = context.FundDistributions.Find(distributionId);
            if (fundDistribution == null)
                return Results.NotFound("Fund distribution not found");

            context.FundDistributions.Remove(fundDistribution);
            context.SaveChanges();
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to delete fund distribution: {ex.Message}");
        }
    }

    public static IResult UpdateDonation(int donationId, Donation updatedDonation)
    {
        try
        {
            using var context = GetContext();
            if (donationId != updatedDonation.DonationId)
                return Results.BadRequest("Donation ID mismatch");

            var existingDonation = context.Donations.AsNoTracking().FirstOrDefault(d => d.DonationId == donationId);
            if (existingDonation == null)
                return Results.NotFound("Donation not found");

            context.Donations.Attach(updatedDonation);
            context.Entry(updatedDonation).State = EntityState.Modified;
            context.SaveChanges();
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to update donation: {ex.Message}");
        }
    }

    public static IResult UpdateDonor(int donorId, Donor updatedDonor)
    {
        try
        {
            using var context = GetContext();
            if (donorId != updatedDonor.DonorId)
                return Results.BadRequest("Donor ID mismatch");

            var existingDonor = context.Donors.AsNoTracking().FirstOrDefault(d => d.DonorId == donorId);
            if (existingDonor == null)
                return Results.NotFound("Donor not found");

            context.Donors.Attach(updatedDonor);
            context.Entry(updatedDonor).State = EntityState.Modified;
            context.SaveChanges();
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to update donor: {ex.Message}");
        }
    }

    public static IResult UpdateFundDistribution(int distributionId, FundDistribution updatedFundDistribution)
    {
        try
        {
            using var context = GetContext();
            if (distributionId != updatedFundDistribution.DistributionId)
                return Results.BadRequest("Distribution ID mismatch");

            var existingFundDistribution = context.FundDistributions.AsNoTracking().FirstOrDefault(fd => fd.DistributionId == distributionId);
            if (existingFundDistribution == null)
                return Results.NotFound("Fund distribution not found");

            context.FundDistributions.Attach(updatedFundDistribution);
            context.Entry(updatedFundDistribution).State = EntityState.Modified;
            context.SaveChanges();
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to update fund distribution: {ex.Message}");
        }
    }

    public static IResult GetCampaignById(int campaignId)
    {
        try
        {
            using var context = GetContext();
            var campaign = context.Campaigns.Find(campaignId);
            return campaign == null ? Results.NotFound("Campaign not found") : Results.Ok(campaign);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to get campaign: {ex.Message}");
        }
    }

    public static IResult GetDonationById(int donationId)
    {
        try
        {
            using var context = GetContext();
            var donation = context.Donations.Find(donationId);
            return donation == null ? Results.NotFound("Donation not found") : Results.Ok(donation);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to get donation: {ex.Message}");
        }
    }

    public static IResult GetDonorById(int donorId)
    {
        try
        {
            using var context = GetContext();
            var donor = context.Donors.Find(donorId);
            return donor == null ? Results.NotFound("Donor not found") : Results.Ok(donor);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to get donor: {ex.Message}");
        }
    }

    public static IResult GetFundDistributionById(int distributionId)
    {
        try
        {
            using var context = GetContext();
            var distribution = context.FundDistributions.Find(distributionId);
            return distribution == null ? Results.NotFound("Fund distribution not found") : Results.Ok(distribution);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to get fund distribution: {ex.Message}");
        }
    }

    public static IResult GetDonationsForCampaign(int campaignId)
    {
        try
        {
            using var context = GetContext();
            var donations = context.Donations.Where(d => d.CampaignId == campaignId).ToList();
            return donations.Any() ? Results.Ok(donations) : Results.NotFound("No donations found for this campaign");
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to get donations: {ex.Message}");
        }
    }

    public static IResult GetFundDistributionsForCampaign(int campaignId)
    {
        try
        {
            using var context = GetContext();
            var distributions = context.FundDistributions.Where(f => f.CampaignId == campaignId).ToList();
            return distributions.Any() ? Results.Ok(distributions) : Results.NotFound("No distributions found for this campaign");
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to get fund distributions: {ex.Message}");
        }
    }

    public static IResult GetDonationsForDonor(int donorId)
    {
        try
        {
            using var context = GetContext();
            var donations = context.Donations.Where(d => d.DonorId == donorId).ToList();
            return donations.Any() ? Results.Ok(donations) : Results.NotFound("No donations found for this donor");
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to get donations: {ex.Message}");
        }
    }

    public static IResult GetFundDistributionsForBeneficiary(string beneficiaryName)
    {
        try
        {
            using var context = GetContext();
            var distributions = context.FundDistributions.Where(f => f.FBeneficiaryName == beneficiaryName).ToList();
            return distributions.Any() ? Results.Ok(distributions) : Results.NotFound("No distributions found for this beneficiary");
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to get fund distributions: {ex.Message}");
        }
    }

    public static IResult GetFundDistributionsForCampaignAndBeneficiary(int campaignId, string beneficiaryName)
    {
        try
        {
            using var context = GetContext();
            var distributions = context.FundDistributions
                .Where(f => f.CampaignId == campaignId && f.FBeneficiaryName == beneficiaryName)
                .ToList();
            return distributions.Any() ? Results.Ok(distributions) : Results.NotFound("No distributions found");
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to get fund distributions: {ex.Message}");
        }
    }

    public static IResult GetTotalDonationsForCampaign(int campaignId)
    {
        try
        {
            using var context = GetContext();
            var total = context.Donations.Where(d => d.CampaignId == campaignId).Sum(d => d.DAmount);
            return Results.Ok(total);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to get total donations: {ex.Message}");
        }
    }

    public static IResult GetTotalDistributionsForCampaign(int campaignId)
    {
        try
        {
            using var context = GetContext();
            var total = context.FundDistributions.Where(f => f.CampaignId == campaignId).Sum(f => f.FAmountDistributed);
            return Results.Ok(total);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to get total distributions: {ex.Message}");
        }
    }

    public static IResult GetTotalDonationsForDonor(int donorId)
    {
        try
        {
            using var context = GetContext();
            var total = context.Donations.Where(d => d.DonorId == donorId).Sum(d => d.DAmount);
            return Results.Ok(total);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to get total donations: {ex.Message}");
        }
    }

    public static IResult GetTotalDistributionsForBeneficiary(string beneficiaryName)
    {
        try
        {
            using var context = GetContext();
            var total = context.FundDistributions.Where(f => f.FBeneficiaryName == beneficiaryName).Sum(f => f.FAmountDistributed);
            return Results.Ok(total);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to get total distributions: {ex.Message}");
        }
    }

    public static IResult GetTotalDistributionsForCampaignAndBeneficiary(int campaignId, string beneficiaryName)
    {
        try
        {
            using var context = GetContext();
            var total = context.FundDistributions
                .Where(f => f.CampaignId == campaignId && f.FBeneficiaryName == beneficiaryName)
                .Sum(f => f.FAmountDistributed);
            return Results.Ok(total);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to get total distributions: {ex.Message}");
        }
    }

    public static IResult GetTotalDonationsForCampaignAndDonor(int campaignId, int donorId)
    {
        try
        {
            using var context = GetContext();
            var total = context.Donations
                .Where(d => d.CampaignId == campaignId && d.DonorId == donorId)
                .Sum(d => d.DAmount);
            return Results.Ok(total);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to get total donations: {ex.Message}");
        }
    }
    // Display the name and phone number of the donor who gives donations greater 1000
    public static IResult ? GetDonorsWithHighDonations()
    {
        try
        {
            using var context = GetContext();
            var donors = context.Donations
                .Where(d => d.DAmount > 1000)
                .Select(d => d.Donor != null ? new { d.Donor.DName, PhoneNumber = d.Donor.DPhone ?? "N/A" } : null)
                .Where(d => d != null)
                .ToList();
            return Results.Ok(donors);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to get donors: {ex.Message}");
        }
    }

}
