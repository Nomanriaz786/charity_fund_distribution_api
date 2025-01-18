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
app.MapGet("/campaigns", () => API_Functions.GetCampaigns());
// Add a campaign
app.MapPost("/campaigns", (Campaign campaign) => API_Functions.AddCampaign(campaign));
// Update the campaign
app.MapPut("/campaigns/{campaignId}", (int campaignId, Campaign campaign) => 
    API_Functions.UpdateCampaign(campaignId, campaign));
// Delete the campaign
app.MapDelete("/campaigns/{campaignId}", (int campaignId) => API_Functions.DeleteCampaign(campaignId));
// Add a donation
app.MapPost("/donations", (Donation donation) => API_Functions.AddDonation(donation));
// Display all donations
app.MapGet("/donations", () => API_Functions.GetDonations());
// Update the donation
app.MapPut("/donations/{donationId}", (int donationId, Donation donation) => 
    API_Functions.UpdateDonation(donationId, donation));
// Delete the donation
app.MapDelete("/donations/{donationId}", (int donationId) => 
    API_Functions.DeleteDonation(donationId));
// Add a fund distribution
app.MapPost("/fund-distributions", (FundDistribution fundDistribution) =>
    API_Functions.AddFundDistribution(fundDistribution));
// Display all fund distributions
app.MapGet("/fund-distributions", () => API_Functions.GetFundDistributions());
// Update the fund distribution
app.MapPut("/fund-distributions/{distributionId}", (int distributionId, FundDistribution distribution) => 
    API_Functions.UpdateFundDistribution(distributionId, distribution));
// Delete the fund distribution
app.MapDelete("/fund-distributions/{distributionId}", (int distributionId) => API_Functions.DeleteFundDistribution(distributionId));
// Add a donor
app.MapPost("/donors", (Donor donor) => API_Functions.AddDonor(donor));
// Display all donors
app.MapGet("/donors", () => API_Functions.GetDonors());
// Update the donor
app.MapPut("/donors/{donorId}", (int donorId, Donor donor) => 
    API_Functions.UpdateDonor(donorId, donor));
// Delete the donor
app.MapDelete("/donors/{donorId}", (int donorId) =>
    API_Functions.DeleteDonor(donorId));
// Get a campaign by ID
app.MapGet("/campaigns/{campaignId}", (int campaignId) => 
    API_Functions.GetCampaignById(campaignId));
// Get a donation by ID
app.MapGet("/donations/{donationId}", (int donationId) => 
    API_Functions.GetDonationById(donationId));
// Get a fund distribution by ID
app.MapGet("/fund-distributions/{distributionId}", (int distributionId) => 
    API_Functions.GetFundDistributionById(distributionId));
// Get a donor by ID
app.MapGet("/donors/{donorId}", (int donorId) => 
    API_Functions.GetDonorById(donorId));
// Get all donations for a campaign
app.MapGet("/campaigns/{campaignId}/donations", (int campaignId) => 
    API_Functions.GetDonationsForCampaign(campaignId));
// Get all fund distributions for a campaign
app.MapGet("/campaigns/{campaignId}/fund-distributions", (int campaignId) => 
    API_Functions.GetFundDistributionsForCampaign(campaignId));
// all donations for a donor
app.MapGet("/donors/{donorId}/donations", (int donorId) => 
    API_Functions.GetDonationsForDonor(donorId));
// Get all fund distributions for a beneficiary
app.MapGet("/beneficiaries/{beneficiaryName}/fund-distributions", (string beneficiaryName) => 
    API_Functions.GetFundDistributionsForBeneficiary(beneficiaryName));
// Get all fund distributions for a campaign and beneficiary
app.MapGet("/campaigns/{campaignId}/beneficiaries/{beneficiaryName}/fund-distributions", 
    (int campaignId, string beneficiaryName) => 
    API_Functions.GetFundDistributionsForCampaignAndBeneficiary(campaignId, beneficiaryName));
// Get the total amount donated for a campaign
app.MapGet("/campaigns/{campaignId}/total-donations", (int campaignId) => 
    API_Functions.GetTotalDonationsForCampaign(campaignId));
// Get the total amount distributed for a campaign
app.MapGet("/campaigns/{campaignId}/total-distributions", (int campaignId) => 
    API_Functions.GetTotalDistributionsForCampaign(campaignId));
// Get the total amount donated by a donor
app.MapGet("/donors/{donorId}/total-donations", (int donorId) => 
    API_Functions.GetTotalDonationsForDonor(donorId));
// Get the total amount distributed to a beneficiary
app.MapGet("/beneficiaries/{beneficiaryName}/total-distributions", (string beneficiaryName) => 
    API_Functions.GetTotalDistributionsForBeneficiary(beneficiaryName));
// Get the total amount distributed to a beneficiary for a campaign
app.MapGet("/campaigns/{campaignId}/beneficiaries/{beneficiaryName}/total-distributions", 
    (int campaignId, string beneficiaryName) => 
    API_Functions.GetTotalDistributionsForCampaignAndBeneficiary(campaignId, beneficiaryName));
// Get the total amount donated for a campaign by a donor
app.MapGet("/campaigns/{campaignId}/donors/{donorId}/total-donations", 
    (int campaignId, int donorId) => 
    API_Functions.GetTotalDonationsForCampaignAndDonor(campaignId, donorId));
app.Run();