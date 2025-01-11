USE Charity_Fundraising_DBMS;
GO

BEGIN TRY
    BEGIN TRANSACTION;

    -- Insert data into Donor table first (parent table)
    INSERT INTO Donor (D_Name, D_Email, D_Phone, D_Address) VALUES
    ('John Smith', 'john.smith@email.com', '555-0101', '123 Main St, City'),
    ('Sarah Johnson', 'sarah.j@email.com', '555-0102', '456 Oak Ave, Town'),
    ('Michael Brown', 'mbrown@email.com', '555-0103', '789 Pine Rd, Village'),
    ('Emma Wilson', 'ewilson@email.com', '555-0104', '321 Elm St, County'),
    ('James Davis', 'jdavis@email.com', '555-0105', '654 Maple Dr, State'),
    ('Lisa Anderson', 'lisa.a@email.com', '555-0106', '987 Cedar Ln, City'),
    ('Robert Taylor', 'rtaylor@email.com', '555-0107', '147 Birch St, Town'),
    ('Patricia White', 'pwhite@email.com', '555-0108', '258 Spruce Ave, Village'),
    ('David Miller', 'dmiller@email.com', '555-0109', '369 Pine St, County'),
    ('Jennifer Lee', 'jlee@email.com', '555-0110', '741 Oak Rd, State');

    -- Insert data into Campaign table (parent table)
    INSERT INTO Campaign (C_Title, C_Description, C_StartDate, C_EndDate, C_GoalAmount, C_Status) VALUES
    ('Education Fund 2023', 'Supporting underprivileged students', '2023-01-01', '2023-12-31', 50000.00, 'Active'),
    ('Medical Aid Program', 'Helping with medical expenses', '2023-02-01', '2023-11-30', 75000.00, 'Active'),
    ('Food Bank Initiative', 'Providing meals to families', '2023-03-01', '2023-10-31', 25000.00, 'Active'),
    ('Elderly Care Project', 'Supporting senior citizens', '2023-04-01', '2023-12-31', 35000.00, 'Active'),
    ('Youth Sports Program', 'Promoting youth athletics', '2023-05-01', '2023-11-30', 20000.00, 'Active'),
    ('Disaster Relief Fund', 'Emergency assistance program', '2023-06-01', '2023-12-31', 100000.00, 'Active'),
    ('Animal Shelter Support', 'Helping local animal shelters', '2023-07-01', '2023-12-31', 15000.00, 'Active'),
    ('Arts Education Fund', 'Supporting arts in schools', '2023-08-01', '2023-12-31', 30000.00, 'Active'),
    ('Environmental Project', 'Local environmental initiatives', '2023-09-01', '2023-12-31', 40000.00, 'Active'),
    ('Community Library Fund', 'Supporting local libraries', '2023-10-01', '2023-12-31', 25000.00, 'Active');

    -- Now insert into Donation table (child table)
    INSERT INTO Donation (DonorID, CampaignID, D_DonationDate, D_Amount, D_PaymentMethod) VALUES
    (1, 1, '2023-01-15', 1000.00, 'Credit Card'),
    (2, 2, '2023-02-20', 1500.00, 'Bank Transfer'),
    (3, 3, '2023-03-25', 750.00, 'PayPal'),
    (4, 4, '2023-04-10', 2000.00, 'Credit Card'),
    (5, 5, '2023-05-15', 500.00, 'Debit Card'),
    (6, 6, '2023-06-20', 3000.00, 'Bank Transfer'),
    (7, 7, '2023-07-25', 250.00, 'PayPal'),
    (8, 8, '2023-08-10', 1000.00, 'Credit Card'),
    (9, 9, '2023-09-15', 1500.00, 'Bank Transfer'),
    (10, 10, '2023-10-20', 750.00, 'PayPal');

    -- Finally insert into FundDistribution table (child table)
    INSERT INTO FundDistribution (CampaignID, F_BeneficiaryName, F_DistributionDate, F_AmountDistributed, F_Purpose) VALUES
    (1, 'Local School District', '2023-03-01', 5000.00, 'School supplies and books'),
    (2, 'City Hospital', '2023-04-01', 7500.00, 'Medical equipment'),
    (3, 'Community Food Bank', '2023-05-01', 2500.00, 'Food supplies'),
    (4, 'Senior Care Center', '2023-06-01', 3500.00, 'Elder care services'),
    (5, 'Youth Sports League', '2023-07-01', 2000.00, 'Sports equipment'),
    (6, 'Emergency Services', '2023-08-01', 10000.00, 'Disaster relief supplies'),
    (7, 'Local Animal Shelter', '2023-09-01', 1500.00, 'Animal care supplies'),
    (8, 'Arts Center', '2023-10-01', 3000.00, 'Art supplies and programs'),
    (9, 'Environmental Group', '2023-11-01', 4000.00, 'Conservation project'),
    (10, 'Public Library', '2023-12-01', 2500.00, 'Books and resources');

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    PRINT ERROR_MESSAGE();
END CATCH
