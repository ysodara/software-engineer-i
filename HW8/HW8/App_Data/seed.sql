
-- ################### SEED DATA ######################

-- Extract data from .csv file and load into our db

-- Create a staging table to hold all the seed data.  We'll query this 
-- table in order to extract what we need to then insert into our real
-- tables.
CREATE TABLE [dbo].[AllData]
(
	[Location] NVARCHAR(50),
	[MeetDate] DATETIME,
	[Team] NVARCHAR(50),
	[Coach] NVARCHAR(50),
	[Event] NVARCHAR(20),
	[Gender] NVARCHAR(20),
	[Athlete] NVARCHAR(50),
	[RaceTime] REAL
);

-- Hard code the full path to the csv file.  It'll be better this way 
-- when we run this in HW 9 to build an Azure db 
BULK INSERT [dbo].[AllData]
	FROM 'C:\Users\PC\Downloads\racetimes.csv'
	WITH
	(
		FIELDTERMINATOR = ',',
		ROWTERMINATOR = '\n',
		FIRSTROW = 2
	);

-- Load coaches data, no foreign keys here to worry about so we can 
-- do a straight insert of just the distinct values
INSERT INTO [dbo].[Coach] ([Name])
	SELECT DISTINCT Coach from [dbo].[AllData];

-- Load Team data, a team has a coach so we need to find the correct entry in the 
-- Coaches table so we can set the foreign key appropriately
INSERT INTO [dbo].[Team]
	(Name,CoachID)
	SELECT DISTINCT ad.Team,cs.ID
		FROM dbo.AllData as ad, dbo.Coach as cs
		WHERE ad.Coach = cs.Name;

INSERT INTO [dbo].[Athlete]
	(Name, Gender, TeamID)
	SELECT DISTINCT ad.Athlete, ad.Gender, teamid.ID
		FROM dbo.AllData as ad, dbo.Team as teamid
		WHERE ad.Team = teamid.Name;
INSERT INTO [dbo].[Location]
	(MeetTitle, Date)
	SELECT DISTINCT ad.Location, ad.MeetDate
		FROM dbo.AllData as ad;

INSERT INTO [dbo].[TimeRecord]	(EventTitle, Time, AthleteID, LocationID)
	Select ad.Event,ad.RaceTime, ath.ID, loc.ID
	FROM dbo.AllData as ad 
							JOIN Athlete ath ON ad.Athlete = ath.Name 
							Join Location loc on ad.Location = loc.MeetTitle
							
							 
	
							
							
 


							

	

	


-- We don't need this staging table anymore so clear it away
DROP TABLE [dbo].[AllData];

