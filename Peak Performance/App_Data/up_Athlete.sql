CREATE TABLE [dbo].[Coaches]
(
	[CoachId] INT NOT NULL IDENTITY(1,1),
	[FirstName] NVARCHAR(100) NOT NULL,
	[LastName] NVARCHAR(100) NOT NULL,
    [PreferredName] NVARCHAR (100),
	[ProfilePic] VARBINARY(max),
	[Active] BIT NOT NULL,
	[UserId] NVARCHAR(128),
	[Email] NVARCHAR(128) NOT NULL,

	--CONSTRAINT [FK_dbo.Coaches_dbo.Users_UserId] FOREIGN KEY ([UserID]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [PK_dbo.Coaches] PRIMARY KEY CLUSTERED ([CoachId] ASC)
);

CREATE TABLE [dbo].[Teams]
(
	[TeamId] INT NOT NULL IDENTITY(1,1),
	[TeamName] NVARCHAR(200) NOT NULL,
	[SportId] INT,
	[CoachId] INT,

	CONSTRAINT [FK_dbo.Teams_dbo.Sports_SportId] FOREIGN KEY ([SportId]) REFERENCES [dbo].[Sports] ([SportId]) ON DELETE SET NULL,
	CONSTRAINT [FK_dbo.Teams_dbo.Coaches_CoachId] FOREIGN KEY ([CoachId]) REFERENCES [dbo].[Coaches] ([CoachId]) ON DELETE CASCADE,
	CONSTRAINT [PK_dbo.Teams] PRIMARY KEY CLUSTERED ([TeamId] ASC)
);

CREATE TABLE [dbo].[Athletes]
(
	[AthleteId] INT NOT NULL IDENTITY(1,1),
	[FirstName] NVARCHAR(100) NOT NULL,
	[LastName] NVARCHAR(100) NOT NULL,
    [PreferredName] NVARCHAR (100),
	[ProfilePic] VARBINARY(max),
	[Sex] CHAR(1),  
	[Gender] NVARCHAR(200),
	[Active] BIT NOT NULL,
	[DOB] DATE NOT NULL,
    [Height] FLOAT,
    [Weight] FLOAT,
	[UserId] NVARCHAR(128),
	[TeamId] INT NOT NULL,
	[Email] NVARCHAR(128) NOT NULL,
	
	--CONSTRAINT [FK_dbo.Athletes_dbo.Users_UserId] FOREIGN KEY ([UserID]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.Athletes_dbo.Teams_TeamId] FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Teams] ([TeamId]) ON DELETE NO ACTION,
	CONSTRAINT [PK_dbo.Athletes] PRIMARY KEY CLUSTERED ([AthleteId] ASC)
);

