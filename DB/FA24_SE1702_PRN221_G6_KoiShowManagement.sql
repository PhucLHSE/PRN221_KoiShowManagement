USE master;
GO
DROP DATABASE IF EXISTS FA24_SE1702_PRN221_G6_KoiShowManagement;
GO
CREATE DATABASE FA24_SE1702_PRN221_G6_KoiShowManagement;
GO

USE FA24_SE1702_PRN221_G6_KoiShowManagement;

-- Create AnimalVarieties Table
CREATE TABLE AnimalVarieties (
    VarietyId INT PRIMARY KEY IDENTITY(1,1),
    VarietyDescription NVARCHAR(MAX), 
    VarietyName NVARCHAR(MAX), 
    IsDeleted BIT DEFAULT 0
);
GO

-- Create Roles Table
CREATE TABLE Roles (
    RoleId INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(50) NOT NULL,
    RoleDescription NVARCHAR(MAX)
);
GO

-- Create Users Table (with RoleId as foreign key)
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(MAX) NOT NULL, 
    Password NVARCHAR(MAX) NOT NULL,
    Email NVARCHAR(MAX) NOT NULL,
    FullName NVARCHAR(MAX),
    PhoneNumber NVARCHAR(15),
    Address NVARCHAR(MAX),
    Status INT,
    DateOfBirth DATE,     
    ProfileImage NVARCHAR(MAX), 
    CreationDate DATETIME DEFAULT GETDATE(),
    RoleId INT, 
    IsDeleted BIT DEFAULT 0,
    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);
GO

-- Create Competitions Table
CREATE TABLE Competitions (
    CompetitionId INT PRIMARY KEY IDENTITY(1,1),
    CompetitionName NVARCHAR(MAX),
    Title NVARCHAR(MAX),
    Description NVARCHAR(MAX),
    StartDate DATETIME,
    EndDate DATETIME,
    Location NVARCHAR(MAX),
    CompetitionType BIT, 
    Status INT,
    NumberOfParticipants INT, 
    Image NVARCHAR(MAX),
    ContactInfo NVARCHAR(MAX),
    ShapePointPercentage INT,
    ColorPointPercentage INT,
    PatternPointPercentage INT,
    RulesDocument NVARCHAR(MAX),        
    JudgingPanel NVARCHAR(MAX),         
    MaxJudges INT,                      
    IsDeleted BIT DEFAULT 0
);
GO

-- Create CompetitionCategories Table (Place it early so it's available for references)
CREATE TABLE CompetitionCategories (
    CategoryId INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(100) NOT NULL, 
    CompetitionId INT NOT NULL,          -- CompetitionId references Competitions
    VarietyId INT NULL,                  
    MinSize INT NULL,                    
    MaxSize INT NULL,                    
    MinAge INT NULL,                     
    MaxAge INT NULL,                     
    JudgingCriteria NVARCHAR(255),       
    WeightPercentageForShape INT NULL,   
    WeightPercentageForColor INT NULL,   
    WeightPercentageForPattern INT NULL, 
    RequiredHealthStatus NVARCHAR(100) NULL,    
    RequiredDocuments NVARCHAR(MAX) NULL,      
    CompetitionFee FLOAT NULL,           
    MaxParticipants INT NULL,            
    PrizeDescription NVARCHAR(MAX),      
    IsDeleted BIT DEFAULT 0,             
    FOREIGN KEY (VarietyId) REFERENCES AnimalVarieties(VarietyId), 
    FOREIGN KEY (CompetitionId) REFERENCES Competitions(CompetitionId) 
);
GO

-- Create Animals Table (with VarietyId as foreign key)
CREATE TABLE Animals (
    AnimalId INT PRIMARY KEY IDENTITY(1,1),
    AnimalName NVARCHAR(MAX),
    VarietyId INT,                         -- VarietyId references AnimalVarieties
    Size INT,
    BirthDate DATETIME,
    Image NVARCHAR(MAX),
    Weight INT, 
    AnimalDescription NVARCHAR(MAX),
    CountryOfOrigin NVARCHAR(MAX),
    HealthStatus NVARCHAR(MAX),
    Gender INT, 
    Rating FLOAT, 
    IsDeleted BIT DEFAULT 0 not null,
    FOREIGN KEY (VarietyId) REFERENCES AnimalVarieties(VarietyId)
);
GO

-- Create Registrations Table (with CompetitionId, AnimalId, and UserId as foreign keys)
CREATE TABLE Registrations (
    RegistrationId INT PRIMARY KEY IDENTITY(1,1),
    CompetitionId INT,                    -- CompetitionId references Competitions
    AnimalId INT,                         -- AnimalId references Animals
    UserId INT,                           -- UserId references Users (Owner who registers the animal)
    EntryTitle NVARCHAR(MAX),
    CheckInStatus BIT, 
    RegistrationDate DATETIME,
    ApprovalStatus BIT,
    Notes NVARCHAR(MAX),
    Image NVARCHAR(MAX),
    HealthStatus NVARCHAR(MAX),
    Color NVARCHAR(MAX),
    Shape NVARCHAR(MAX),
    Pattern NVARCHAR(MAX),
    IsDeleted BIT DEFAULT 0,
    FOREIGN KEY (CompetitionId) REFERENCES Competitions(CompetitionId),
    FOREIGN KEY (AnimalId) REFERENCES Animals(AnimalId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
GO

-- Create PointOnProgressing Table (formerly JudgingPoints) 
-- with JuryId and RegistrationId as foreign keys
CREATE TABLE PointOnProgressing (
    PointId INT PRIMARY KEY IDENTITY(1,1),
    ShapePoint INT NULL,
    ColorPoint INT NULL,
    PatternPoint INT NULL,
    Comment NVARCHAR(MAX) NULL,
    JuryId INT,                          -- JuryId references Users
    RegistrationId INT,                  -- RegistrationId references Registrations
    PointStatus INT,
    JudgeRank NVARCHAR(MAX),
    PenaltyPoints INT, 
    TotalScore INT,
    IsDeleted BIT DEFAULT 0,
    CategoryId INT,                      -- CategoryId references CompetitionCategories
    FOREIGN KEY (JuryId) REFERENCES Users(UserId),
    FOREIGN KEY (RegistrationId) REFERENCES Registrations(RegistrationId),
    FOREIGN KEY (CategoryId) REFERENCES CompetitionCategories(CategoryId)
);
GO

-- Create FinalResults Table (formerly CompetitionResults)
-- with CompetitionId as foreign key
CREATE TABLE FinalResults (
    CompetitionResultId INT PRIMARY KEY IDENTITY(1,1),
    CompetitionId INT,                   -- CompetitionId references Competitions
    ResultName NVARCHAR(MAX),
    ResultDescription NVARCHAR(MAX),
    TotalScore FLOAT,
    Rank INT,
    Comments NVARCHAR(MAX),
    IsFinalized BIT,
    IsPublished BIT,
    Category NVARCHAR(MAX),
    Status BIT,
    PrizeAmount INT,
    PrizeDescription NVARCHAR(MAX),
    IsDeleted BIT DEFAULT 0,
    CategoryId INT,                      -- CategoryId references CompetitionCategories
    FOREIGN KEY (CompetitionId) REFERENCES Competitions(CompetitionId),
    FOREIGN KEY (CategoryId) REFERENCES CompetitionCategories(CategoryId)
);
GO

-- Create Payments Table (with RegistrationId as foreign key)
CREATE TABLE Payments (
    PaymentId INT PRIMARY KEY IDENTITY(1,1),
    RegistrationId INT,                  -- RegistrationId references Registrations
    TransactionId NVARCHAR(MAX),
    PaymentAmount FLOAT,
    PaymentDate DATETIME,
    PaymentStatus NVARCHAR(MAX),
    Description NVARCHAR(MAX),
    VATRate FLOAT,
    ActualCost FLOAT,
    DiscountAmount FLOAT,
    FinalAmount FLOAT,
    Currency NVARCHAR(MAX),
    PaymentMethod NVARCHAR(100),         
    FOREIGN KEY (RegistrationId) REFERENCES Registrations(RegistrationId)
);
GO

-- Create Feedbacks Table
CREATE TABLE Feedbacks (
    FeedbackId INT PRIMARY KEY IDENTITY(1,1),
    CompetitionId INT,                   -- CompetitionId references Competitions
    UserId INT,                          -- UserId references Users
    Rating INT,                         
    Comments NVARCHAR(MAX),             
    FeedbackDate DATETIME DEFAULT GETDATE(),
    IsDeleted BIT DEFAULT 0,            
    Response NVARCHAR(MAX),             
    ResponseDate DATETIME,              
    FeedbackType NVARCHAR(50),          
    Status INT DEFAULT 0,               
    IsAnonymous BIT DEFAULT 0,          
    VisibilityLevel NVARCHAR(50),       
    SeverityLevel NVARCHAR(50),         
    IsResponded BIT DEFAULT 0,          
    Platform NVARCHAR(50),              
    FOREIGN KEY (CompetitionId) REFERENCES Competitions(CompetitionId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
GO

INSERT INTO AnimalVarieties (VarietyDescription, VarietyName, IsDeleted)
VALUES 
    ('A koi variety with vibrant red and white patterns', 'Kohaku', 0),
    ('A koi variety with yellowish-orange scales', 'Kigoi', 0),
    ('A koi variety with metallic white and red scales', 'Platinum Ogon', 0);
Go
INSERT INTO Roles (RoleName, RoleDescription)
VALUES 
    ('Admin', 'Administrator with full access'),
    ('Judge', 'User responsible for judging the competition'),
    ('Participant', 'User who participates in the competition');
Go
INSERT INTO Users (Username, Password, Email, FullName, PhoneNumber, Address, Status, DateOfBirth, ProfileImage, RoleId)
VALUES 
    ('admin_user', 'securepassword', 'admin@example.com', 'Admin User', '1234567890', '123 Admin St', 1, '1985-05-15', 'admin.jpg', 1),
    ('judge_user', 'securepassword', 'judge@example.com', 'Judge User', '0987654321', '456 Judge St', 1, '1990-08-20', 'judge.jpg', 2),
    ('participant_user', 'securepassword', 'participant@example.com', 'Participant User', '1122334455', '789 Participant St', 1, '1995-12-10', 'participant.jpg', 3);
Go
INSERT INTO Competitions (CompetitionName, Title, Description, StartDate, EndDate, Location, CompetitionType, Status, NumberOfParticipants, ContactInfo, ShapePointPercentage, ColorPointPercentage, PatternPointPercentage, RulesDocument, JudgingPanel, MaxJudges)
VALUES 
    ('National Koi Show', 'Annual National Koi Show', 'The largest koi show in the region', '2024-10-01', '2024-10-05', 'Koi Garden Arena', 1, 1, 50, 'contact@koishow.com', 30, 30, 40, 'rules.pdf', 'Panel A', 5),
    ('Regional Koi Championship', 'Regional Koi Competition', 'The regional koi competition for expert breeders', '2024-08-15', '2024-08-17', 'Regional Pond Park', 0, 1, 25, 'info@regionalkoishow.com', 35, 30, 35, 'regional_rules.pdf', 'Panel B', 3);
Go
INSERT INTO CompetitionCategories (CategoryName, CompetitionId, VarietyId, MinSize, MaxSize, MinAge, MaxAge, JudgingCriteria, WeightPercentageForShape, WeightPercentageForColor, WeightPercentageForPattern, RequiredHealthStatus, RequiredDocuments, CompetitionFee, MaxParticipants, PrizeDescription)
VALUES 
    ('Small Kohaku', 1, 1, 10, 20, 1, 3, 'Shape, Color, and Pattern', 30, 30, 40, 'Healthy', 'Health certificate', 100.00, 50, 'First Prize: Trophy and $500'),
    ('Large Kigoi', 2, 2, 30, 50, 3, 5, 'Focus on shape and color', 35, 30, 35, 'Healthy', 'Health certificate', 150.00, 25, 'First Prize: Trophy and $700');
Go
INSERT INTO Animals (AnimalName, VarietyId, Size, BirthDate, Image, Weight, AnimalDescription, CountryOfOrigin, HealthStatus, Gender, Rating)
VALUES 
    ('Kohaku King', 1, 15, '2021-05-01', 'kohaku_king.jpg', 5, 'Strong and vibrant Kohaku', 'Japan', 'Healthy', 1, 9.5),
    ('Golden Glow', 2, 35, '2019-07-10', 'golden_glow.jpg', 15, 'Bright yellow Kigoi with great symmetry', 'Japan', 'Healthy', 1, 8.8);
Go
-- Chèn vào bảng Registrations, đảm bảo AnimalId tồn tại trong bảng Animals
INSERT INTO Registrations (CompetitionId, AnimalId, UserId, EntryTitle, CheckInStatus, RegistrationDate, ApprovalStatus, Notes, Image, HealthStatus, Color, Shape, Pattern)
VALUES 
    (1, 1, 3, 'Best Kohaku', 1, '2024-09-01', 1, 'Ready for competition', 'kohaku_entry.jpg', 'Healthy', 'Red and white', 'Oval', 'Checkerboard'),
    (2, 2, 3, 'Golden Beauty', 0, '2024-08-10', 0, 'Pending approval', 'kigoi_entry.jpg', 'Healthy', 'Yellow', 'Rounded', 'Solid');

Go
INSERT INTO PointOnProgressing (ShapePoint, ColorPoint, PatternPoint, Comment, JuryId, RegistrationId, PointStatus, JudgeRank, PenaltyPoints, TotalScore, CategoryId)
VALUES 
    (28, 29, 38, 'Excellent specimen with vibrant colors', 2, 3, 1, 'Head Judge', 0, 95, 1),
    (30, 30, 40, 'Flawless shape and color', 2, 2, 1, 'Head Judge', 0, 100, 2);
Go
INSERT INTO FinalResults (CompetitionId, ResultName, ResultDescription, TotalScore, Rank, Comments, IsFinalized, IsPublished, Category, Status, PrizeAmount, PrizeDescription, CategoryId)
VALUES 
    (1, 'Best in Show', 'Winner of the Kohaku category', 95.0, 1, 'Excellent koi', 1, 1, 'Kohaku', 1, 500, 'Trophy and $500', 1),
    (2, 'Runner Up', 'Second best in Kigoi category', 88.5, 2, 'Very strong specimen', 1, 1, 'Kigoi', 1, 300, 'Trophy and $300', 2);
Go
INSERT INTO Payments (RegistrationId, TransactionId, PaymentAmount, PaymentDate, PaymentStatus, Description, VATRate, ActualCost, DiscountAmount, FinalAmount, Currency, PaymentMethod)
VALUES 
    (3, 'TXN12345', 100.00, '2024-08-30', 'Completed', 'Entry fee payment for Kohaku competition', 10.0, 90.0, 0.0, 100.00, 'USD', 'Credit Card'),
    (2, 'TXN67890', 150.00, '2024-08-10', 'Pending', 'Entry fee payment for Kigoi competition', 10.0, 135.0, 0.0, 150.00, 'USD', 'PayPal');
Go
INSERT INTO Feedbacks (CompetitionId, UserId, Rating, Comments, Response, ResponseDate, FeedbackType, VisibilityLevel, SeverityLevel, Platform)
VALUES 
    (1, 3, 5, 'Great event! Very well organized.', 'Thank you for your feedback!', '2024-09-02', 'Event', 'Public', 'Low', 'Mobile'),
    (2, 3, 4, 'Good competition but could improve the judging panel.', 'We will review your suggestion.', '2024-09-03', 'Competition', 'Public', 'Medium', 'Web');
Go

