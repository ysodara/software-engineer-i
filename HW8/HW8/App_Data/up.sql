
CREATE TABLE [dbo].[Coach]
(
	[ID]				INT IDENTITY(1,1)	NOT NULL,
	[Name]				NVARCHAR (50)		NOT NULL,
	CONSTRAINT	[PK_dbo.Coach]		PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[Team]
(
	[ID]				INT IDENTITY(1,1)	NOT NULL,
	[Name]				NVARCHAR (50)		NOT NULL,
	[CoachID]			INT,
	CONSTRAINT	[PK_dbo.Team]		PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT	[FK_dbo.Team_dbo.Coach_ID]	FOREIGN KEY ([CoachID]) REFERENCES [dbo].[Coach] ([ID])
);

CREATE TABLE [dbo].[Athlete]
(
	[ID]				INT IDENTITY(1,1)		NOT NULL,
	[Name]				NVARCHAR (50)			NOT NULL,
	[Gender]			NVARCHAR (10)			NOT NULL,
	[TeamID]			INT,
	CONSTRAINT	[PK_dbo.Athlete]		PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT	[FK_dbo.Athlete_dbo.Team_ID]	FOREIGN KEY ([TeamID]) REFERENCES [dbo].[Team]([ID])
);
CREATE TABLE [dbo].[Location]
(
	[ID]				INT IDENTITY(1001,1)	NOT NULL,
	[MeetTitle]			NVARCHAR (50)			NOT NULL,
	[Date]				DATETIME				NOT NULL,
	CONSTRAINT	[PK_dbo.Location]		PRIMARY KEY CLUSTERED ([ID] ASC)
	
);
CREATE TABLE [dbo].[TimeRecord]
(
	[ID]				INT IDENTITY(1001,1)	NOT NULL,
	[EventTitle]		NVARCHAR (10)			NOT NULL,
	[Time]				FLOAT					NOT NULL,

	
	[AthleteID]			INT,
	[LocationID]		INT,
	CONSTRAINT	[PK_dbo.TimeRecord]		PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT	[FK_dbo.TimeRecord_dbo.Athlete_ID]	FOREIGN KEY ([AthleteID]) REFERENCES [dbo].[Athlete]([ID]),
	CONSTRAINT	[FK_dbo.TimeRecord_dbo.Location_ID]	FOREIGN KEY ([LocationID]) REFERENCES [dbo].[Location]([ID])
	
);
