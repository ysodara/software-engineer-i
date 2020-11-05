CREATE TABLE [dbo].[Assignments]
(
	[ID]				INT IDENTITY (1,1)	NOT NULL,
	[PriorityHW]		INT					NOT NULL,
	[DueDate]			DATE				NOT NULL,
	[DueTime]			TIME				NOT NULL,
	[Department]		NVARCHAR(3)			NOT NULL,
	[CourseNum]			INT					NOT NULL,
	[HomeworkTitle]		NVARCHAR(MAX)		NOT NULL,
	[Notes]				NVARCHAR(MAX)		NULL

	CONSTRAINT [PK_dbo.Assignments] PRIMARY KEY CLUSTERED ([ID] ASC)
);

INSERT INTO [dbo].[Assignments] (PriorityHW, DueDate, DueTime, Department, CourseNum, HomeworkTitle) VALUES
	(5,'2019-11-05','10:00', 'CS', 460, 'HW5'),
	(3,'2019-11-14','12:00:00', 'CS', 360, 'LAB4'),
	(2,'2019-11-19','23:00:00', 'LIT', 101, 'Journal 5'),
	(2,'2019-11-30','23:00:00', 'LIT', 101, 'Essay'),
	(5,'2019-11-14','10:00:00', 'CS', 460, 'Homework 6')
GO