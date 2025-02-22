﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Charity_Fundraising_DBMS;

public partial class CharityFundraisingDbmsContext : DbContext
{
    private readonly IConfiguration _configuration;

    public CharityFundraisingDbmsContext(DbContextOptions<CharityFundraisingDbmsContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Campaign> Campaigns { get; set; }

    public virtual DbSet<Donation> Donations { get; set; }

    public virtual DbSet<Donor> Donors { get; set; }

    public virtual DbSet<FundDistribution> FundDistributions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            if (connectionString != null)
            {
                connectionString = connectionString
                    .Replace("${DB_SERVER}", Environment.GetEnvironmentVariable("DB_SERVER") ?? "charitydb")
                    .Replace("${DB_NAME}", Environment.GetEnvironmentVariable("DB_NAME") ?? "Charity_Fundraising_DBMS")
                    .Replace("${DB_USER}", Environment.GetEnvironmentVariable("DB_USER") ?? "sa")
                    .Replace("${DB_PASSWORD}", Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "NomANRIAZ@90");
            }
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Campaign>(entity =>
        {
            entity.HasKey(e => e.CampaignId).HasName("PK__Campaign__3F5E8D79B63E0A00");

            entity.ToTable("Campaign");

            entity.Property(e => e.CampaignId).HasColumnName("CampaignID");
            entity.Property(e => e.CDescription).HasColumnName("C_Description");
            entity.Property(e => e.CEndDate)
                .HasColumnType("datetime")
                .HasColumnName("C_EndDate");
            entity.Property(e => e.CGoalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("C_GoalAmount");
            entity.Property(e => e.CStartDate)
                .HasColumnType("datetime")
                .HasColumnName("C_StartDate");
            entity.Property(e => e.CStatus)
                .HasMaxLength(20)
                .HasColumnName("C_Status");
            entity.Property(e => e.CTitle)
                .HasMaxLength(100)
                .HasColumnName("C_Title");
        });

        modelBuilder.Entity<Donation>(entity =>
        {
            entity.HasKey(e => e.DonationId).HasName("PK__Donation__C5082EDBC4B93929");

            entity.ToTable("Donation");

            entity.Property(e => e.DonationId).HasColumnName("DonationID");
            entity.Property(e => e.CampaignId).HasColumnName("CampaignID");
            entity.Property(e => e.DAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("D_Amount");
            entity.Property(e => e.DDonationDate).HasColumnName("D_DonationDate");
            entity.Property(e => e.DPaymentMethod)
                .HasMaxLength(50)
                .HasColumnName("D_PaymentMethod");
            entity.Property(e => e.DonorId).HasColumnName("DonorID");

            entity.HasOne(d => d.Campaign).WithMany(p => p.Donations)
                .HasForeignKey(d => d.CampaignId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Donation_Campaign");

            entity.HasOne(d => d.Donor).WithMany(p => p.Donations)
                .HasForeignKey(d => d.DonorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Donation_Donor");
        });

        modelBuilder.Entity<Donor>(entity =>
        {
            entity.HasKey(e => e.DonorId).HasName("PK__Donor__052E3F989EECA67B");

            entity.ToTable("Donor");

            entity.Property(e => e.DonorId).HasColumnName("DonorID");
            entity.Property(e => e.DAddress)
                .HasMaxLength(255)
                .HasColumnName("D_Address");
            entity.Property(e => e.DEmail)
                .HasMaxLength(100)
                .HasColumnName("D_Email");
            entity.Property(e => e.DName)
                .HasMaxLength(100)
                .HasColumnName("D_Name");
            entity.Property(e => e.DPhone)
                .HasMaxLength(15)
                .HasColumnName("D_Phone");
        });

        modelBuilder.Entity<FundDistribution>(entity =>
        {
            entity.HasKey(e => e.DistributionId).HasName("PK__FundDist__3226272F80A08F6E");

            entity.ToTable("FundDistribution");

            entity.Property(e => e.DistributionId).HasColumnName("DistributionID");
            entity.Property(e => e.CampaignId).HasColumnName("CampaignID");
            entity.Property(e => e.FAmountDistributed)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("F_AmountDistributed");
            entity.Property(e => e.FBeneficiaryName)
                .HasMaxLength(100)
                .HasColumnName("F_BeneficiaryName");
            entity.Property(e => e.FDistributionDate).HasColumnName("F_DistributionDate");
            entity.Property(e => e.FPurpose).HasColumnName("F_Purpose");

            entity.HasOne(d => d.Campaign).WithMany(p => p.FundDistributions)
                .HasForeignKey(d => d.CampaignId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FundDistribution_Campaign");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
