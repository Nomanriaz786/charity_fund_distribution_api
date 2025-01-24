create database Charity_Fundraising_DBMS
use Charity_Fundraising_DBMS

CREATE TABLE Donor (
    DonorID INT IDENTITY(1,1) PRIMARY KEY,
    D_Name NVARCHAR(100) NOT NULL,
    D_Email NVARCHAR(100),
    D_Phone NVARCHAR(15),
    D_Address NVARCHAR(255)
);

CREATE TABLE Campaign (
    CampaignID INT IDENTITY(1,1) PRIMARY KEY,
    C_Title NVARCHAR(100) NOT NULL,
    C_Description NVARCHAR(MAX),
    C_StartDate DATETIME NOT NULL,
    C_EndDate DATETIME NOT NULL,
    C_GoalAmount DECIMAL(10, 2) NOT NULL,
    C_Status NVARCHAR(20) NOT NULL
);

CREATE TABLE Donation (
    DonationID INT IDENTITY(1,1) PRIMARY KEY,
    DonorID INT NOT NULL,
    CampaignID INT NOT NULL,
    D_DonationDate DATETIME NOT NULL,
    D_Amount DECIMAL(10, 2) NOT NULL,
    D_PaymentMethod NVARCHAR(50),
    CONSTRAINT FK_Donation_Donor FOREIGN KEY (DonorID) REFERENCES Donor(DonorID),
    CONSTRAINT FK_Donation_Campaign FOREIGN KEY (CampaignID) REFERENCES Campaign(CampaignID)
);

CREATE TABLE FundDistribution (
    DistributionID INT IDENTITY(1,1) PRIMARY KEY,
    CampaignID INT NOT NULL,
    F_BeneficiaryName NVARCHAR(100) NOT NULL,
    F_DistributionDate DATETIME NOT NULL,
    F_AmountDistributed DECIMAL(10, 2) NOT NULL,
    F_Purpose NVARCHAR(MAX),
    CONSTRAINT FK_FundDistribution_Campaign FOREIGN KEY (CampaignID) REFERENCES Campaign(CampaignID)
);