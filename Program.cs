using Charity_Fundraising_DBMS;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureHttpJsonOptions(options => {
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.PropertyNameCaseInsensitive = true;
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "Charity_Fundraising_DBMS API", 
        Description = "Making the Charity_Fundraising_DBMS you love",  
        Version = "v1" 
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI(c =>
   {
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "Charity_Fundraising_DBMS API V1");
   });
}
app.MapGet("/", () => "Hello World!");
// Display all campaigns
app.MapGet("/campaigns", () => {
    var api = new API_Functions();
    return Results.Ok(api.GetCampaigns());
});
// Add a campaign
app.MapPost("/campaigns", (Campaign campaign) =>
{
    if (campaign == null)
        return Results.BadRequest("Invalid campaign data");

    var api = new API_Functions();
    var result = api.AddCampaign(campaign);

    return result.Success
        ? Results.Created($"/campaigns/{campaign.CampaignId}", campaign)
        : Results.BadRequest(result.Message);
})
.Produces<Campaign>(StatusCodes.Status201Created)
.Produces(StatusCodes.Status400BadRequest)
.WithName("CreateCampaign")
.WithOpenApi();
app.MapDelete("/campaigns/{campaignId}", (int campaignId) =>
{
    var api = new API_Functions();
    var result = api.DeleteCampaign(campaignId);

    return result.Success
        ? Results.NoContent()
        : Results.BadRequest(result.Message);
});
// Add a donation
app.MapPost("/donations", (Donation donation) =>
{
    if (donation == null)
        return Results.BadRequest("Invalid donation data");

    var api = new API_Functions();
    var result = api.AddDonation(donation);

    return result.Success
        ? Results.Created($"/donations/{donation.DonationId}", donation)
        : Results.BadRequest(result.Message);
})
.Produces<Donation>(StatusCodes.Status201Created)
.Produces(StatusCodes.Status400BadRequest)
.WithName("CreateDonation")
.WithOpenApi();
// Display all donations
app.MapGet("/donations", () => {
    var api = new API_Functions();
    return Results.Ok(api.GetDonations());
});
// Delete the donation
app.MapDelete("/donations/{donationId}", (int donationId) =>
{
    var api = new API_Functions();
    var result = api.DeleteDonation(donationId);

    return result.Success
        ? Results.NoContent()
        : Results.BadRequest(result.Message);
});
// Add a fund distribution
app.MapPost("/fund-distributions", (FundDistribution fundDistribution) =>
{
    if (fundDistribution == null)
        return Results.BadRequest("Invalid fund distribution data");

    var api = new API_Functions();
    var result = api.AddFundDistribution(fundDistribution);

    return result.Success
        ? Results.Created($"/fund-distributions/{fundDistribution.DistributionId}", fundDistribution)
        : Results.BadRequest(result.Message);
})
.Produces<FundDistribution>(StatusCodes.Status201Created)
.Produces(StatusCodes.Status400BadRequest)
.WithName("CreateFundDistribution")
.WithOpenApi();
// Display all fund distributions
app.MapGet("/fund-distributions", () => {
    var api = new API_Functions();
    return Results.Ok(api.GetFundDistributions());
});
// Delete the fund distribution
app.MapDelete("/fund-distributions/{distributionId}", (int distributionId) =>
{
    var api = new API_Functions();
    var result = api.DeleteFundDistribution(distributionId);

    return result.Success
        ? Results.NoContent()
        : Results.BadRequest(result.Message);
});
// Add a donor
app.MapPost("/donors", (Donor donor) =>
{
    if (donor == null)
        return Results.BadRequest("Invalid donor data");

    var api = new API_Functions();
    var result = api.AddDonor(donor);

    return result.Success
        ? Results.Created($"/donors/{donor.DonorId}", donor)
        : Results.BadRequest(result.Message);
})
.Produces<Donor>(StatusCodes.Status201Created)
.Produces(StatusCodes.Status400BadRequest)
.WithName("CreateDonor")
.WithOpenApi();
// Display all donors
app.MapGet("/donors", () => {
    var api = new API_Functions();
    return Results.Ok(api.GetDonors());
});
// Delete the donor
app.MapDelete("/donors/{donorId}", (int donorId) =>
{
    var api = new API_Functions();
    var result = api.DeleteDonor(donorId);

    return result.Success
        ? Results.NoContent()
        : Results.BadRequest(result.Message);
});
// Get a campaign by ID
app.MapGet("/campaigns/{campaignId}", (int campaignId) =>
{
    var api = new API_Functions();
    var campaign = api.GetCampaignById(campaignId);

    return campaign == null
        ? Results.NotFound()
        : Results.Ok(campaign);
});
// Get a donation by ID
app.MapGet("/donations/{donationId}", (int donationId) =>
{
    var api = new API_Functions();
    var donation = api.GetDonationById(donationId);

    return donation == null
        ? Results.NotFound()
        : Results.Ok(donation);
});
// Get a fund distribution by ID
app.MapGet("/fund-distributions/{distributionId}", (int distributionId) =>
{
    var api = new API_Functions();
    var fundDistribution = api.GetFundDistributionById(distributionId);

    return fundDistribution == null
        ? Results.NotFound()
        : Results.Ok(fundDistribution);
});
// Get a donor by ID
app.MapGet("/donors/{donorId}", (int donorId) =>
{
    var api = new API_Functions();
    var donor = api.GetDonorById(donorId);

    return donor == null
        ? Results.NotFound()
        : Results.Ok(donor);
});
// Update the campaign
app.MapPut("/campaigns", (Campaign campaign) =>
{
    if (campaign == null)
        return Results.BadRequest("Invalid campaign data");

    var api = new API_Functions();
    var result = api.UpdateCampaign(campaign);

    return result.Success
        ? Results.NoContent()
        : Results.BadRequest(result.Message);
});
// Update the donation
app.MapPut("/donations", (Donation donation) =>
{
    if (donation == null)
        return Results.BadRequest("Invalid donation data");

    var api = new API_Functions();
    var result = api.UpdateDonation(donation);

    return result.Success
        ? Results.NoContent()
        : Results.BadRequest(result.Message);
});
// Update the donor
app.MapPut("/donors", (Donor donor) =>
{
    if (donor == null)
        return Results.BadRequest("Invalid donor data");

    var api = new API_Functions();
    var result = api.UpdateDonor(donor);

    return result.Success
        ? Results.NoContent()
        : Results.BadRequest(result.Message);
});
// Update the fund distribution
app.MapPut("/fund-distributions", (FundDistribution fundDistribution) =>
{
    if (fundDistribution == null)
        return Results.BadRequest("Invalid fund distribution data");

    var api = new API_Functions();
    var result = api.UpdateFundDistribution(fundDistribution);

    return result.Success
        ? Results.NoContent()
        : Results.BadRequest(result.Message);
});
// Get all donations for a campaign
app.MapGet("/campaigns/{campaignId}/donations", (int campaignId) =>
{
    var api = new API_Functions();
    var donations = api.GetDonationsForCampaign(campaignId);

    return donations == null
        ? Results.NotFound()
        : Results.Ok(donations);
});
// Get all fund distributions for a campaign
app.MapGet("/campaigns/{campaignId}/fund-distributions", (int campaignId) =>
{
    var api = new API_Functions();
    var fundDistributions = api.GetFundDistributionsForCampaign(campaignId);

    return fundDistributions == null
        ? Results.NotFound()
        : Results.Ok(fundDistributions);
});
// all donations for a donor
app.MapGet("/donors/{donorId}/donations", (int donorId) =>
{
    var api = new API_Functions();
    var donations = api.GetDonationsForDonor(donorId);

    return donations == null
        ? Results.NotFound()
        : Results.Ok(donations);
});
// Get all fund distributions for a beneficiary
app.MapGet("/beneficiaries/{beneficiaryName}/fund-distributions", (string beneficiaryName) =>
{
    var api = new API_Functions();
    var fundDistributions = api.GetFundDistributionsForBeneficiary(beneficiaryName);

    return fundDistributions == null
        ? Results.NotFound()
        : Results.Ok(fundDistributions);
});
// Get all fund distributions for a campaign and beneficiary
app.MapGet("/campaigns/{campaignId}/beneficiaries/{beneficiaryName}/fund-distributions", (int campaignId, string beneficiaryName) =>
{
    var api = new API_Functions();
    var fundDistributions = api.GetFundDistributionsForCampaignAndBeneficiary(campaignId, beneficiaryName);

    return fundDistributions == null
        ? Results.NotFound()
        : Results.Ok(fundDistributions);
});
// Get the total amount donated for a campaign
app.MapGet("/campaigns/{campaignId}/total-donations", (int campaignId) =>
{
    var api = new API_Functions();
    var donations = api.GetDonationsForCampaign(campaignId);
    decimal total = 0;

    foreach (var donation in donations)
    {
        total += donation.DAmount;
    }

    return Results.Ok(total);
});
// Get the total amount distributed for a campaign
app.MapGet("/campaigns/{campaignId}/total-distributions", (int campaignId) =>
{
    var api = new API_Functions();
    var fundDistributions = api.GetFundDistributionsForCampaign(campaignId);
    decimal total = 0;

    foreach (var fundDistribution in fundDistributions)
    {
        total += fundDistribution.FAmountDistributed;
    }

    return Results.Ok(total);
});
// Get the total amount donated by a donor
app.MapGet("/donors/{donorId}/total-donations", (int donorId) =>
{
    var api = new API_Functions();
    var donations = api.GetDonationsForDonor(donorId);
    decimal total = 0;

    foreach (var donation in donations)
    {
        total += donation.DAmount;
    }

    return Results.Ok(total);
});
// Get the total amount distributed to a beneficiary
app.MapGet("/beneficiaries/{beneficiaryName}/total-distributions", (string beneficiaryName) =>
{
    var api = new API_Functions();
    var fundDistributions = api.GetFundDistributionsForBeneficiary(beneficiaryName);
    decimal total = 0;

    foreach (var fundDistribution in fundDistributions)
    {
        total += fundDistribution.FAmountDistributed;
    }

    return Results.Ok(total);
});
// Get the total amount distributed to a beneficiary for a campaign
app.MapGet("/campaigns/{campaignId}/beneficiaries/{beneficiaryName}/total-distributions", (int campaignId, string beneficiaryName) =>
{
    var api = new API_Functions();
    var fundDistributions = api.GetFundDistributionsForCampaignAndBeneficiary(campaignId, beneficiaryName);
    decimal total = 0;

    foreach (var fundDistribution in fundDistributions)
    {
        total += fundDistribution.FAmountDistributed;
    }

    return Results.Ok(total);
});
// Get the total amount donated for a campaign by a donor
app.MapGet("/campaigns/{campaignId}/donors/{donorId}/total-donations", (int campaignId, int donorId) =>
{
    var api = new API_Functions();
    var donations = api.GetDonationsForCampaign(campaignId);
    decimal total = 0;

    foreach (var donation in donations)
    {
        if (donation.DonorId == donorId)
            total += donation.DAmount;
    }

    return Results.Ok(total);
});
app.Run();