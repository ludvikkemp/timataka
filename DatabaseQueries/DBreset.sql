DROP TABLE dbo.ContestantsInHeats;

DROP TABLE dbo.Heats;

DROP TABLE dbo.Events;

DROP TABLE dbo.CompetitionInstances;

DROP TABLE dbo.ManagesCompetitions;

DROP TABLE dbo.Competitions;

DROP TABLE dbo.Courses;

DROP TABLE dbo.Disciplines;

DROP TABLE dbo.UsersInClubs

DROP TABLE dbo.Clubs

DROP TABLE dbo.Sports;

CREATE TABLE [dbo].[Sports] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Sports] PRIMARY KEY CLUSTERED ([Id] ASC)
);

SET IDENTITY_INSERT [dbo].[Sports] ON
INSERT INTO [dbo].[Sports] ([Id], [Name]) VALUES ('1','Athletics')
INSERT INTO [dbo].[Sports] ([Id], [Name]) VALUES ('2','Cycling')
INSERT INTO [dbo].[Sports] ([Id], [Name]) VALUES ('3','Swimming')
INSERT INTO [dbo].[Sports] ([Id], [Name]) VALUES ('4','Skiing')
INSERT INTO [dbo].[Sports] ([Id], [Name]) VALUES ('5','Sled Dog Racing')
INSERT INTO [dbo].[Sports] ([Id], [Name]) VALUES ('6','Triathlon')
INSERT INTO [dbo].[Sports] ([Id], [Name]) VALUES ('7','Biathlon')
SET IDENTITY_INSERT [dbo].[Sports] OFF

CREATE TABLE [dbo].[Clubs] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Email]            NVARCHAR (MAX) NULL,
    [Name]             NVARCHAR (MAX) NULL,
    [NameAbbreviation] NVARCHAR (MAX) NULL,
    [Phone]            NVARCHAR (MAX) NULL,
    [Webpage]          NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Clubs] PRIMARY KEY CLUSTERED ([Id] ASC)
);

INSERT INTO [dbo].[Clubs] ([Name], [NameAbbreviation], [Phone], [Webpage], [Email]) VALUES ('Íþróttafélag Reykjavíkur', 'ÍR', '5877080', 'www.ir.is', 'ir@ir.is')
INSERT INTO [dbo].[Clubs] ([Name], [NameAbbreviation], [Phone], [Webpage], [Email]) VALUES ('Knattspyrnufélag Reykjavíkur', 'KR', '1234567', 'www.kr.is', 'kr@kr.is')

CREATE TABLE [dbo].[UsersInClubs] (
    [UserId]  NVARCHAR (450) NOT NULL,
    [SportId] INT            NOT NULL,
    [ClubId]  INT            NOT NULL,
    [Role]    INT            NOT NULL,
    CONSTRAINT [PK_UsersInClubs] PRIMARY KEY CLUSTERED ([UserId] ASC, [SportId] ASC),
    CONSTRAINT [AK_UsersInClubs_SportId_UserId] UNIQUE NONCLUSTERED ([SportId] ASC, [UserId] ASC),
    CONSTRAINT [FK_UsersInClubs_Sports_ClubId] FOREIGN KEY ([ClubId]) REFERENCES [dbo].[Sports] ([Id]),
    CONSTRAINT [FK_UsersInClubs_Sports_SportId] FOREIGN KEY ([SportId]) REFERENCES [dbo].[Sports] ([Id]),
    CONSTRAINT [FK_UsersInClubs_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UsersInClubs_ClubId]
    ON [dbo].[UsersInClubs]([ClubId] ASC);


CREATE TABLE [dbo].[Disciplines] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (MAX) NULL,
    [SportId] INT            NOT NULL,
    CONSTRAINT [PK_Disciplines] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Disciplines_Sports_SportId] FOREIGN KEY ([SportId]) REFERENCES [dbo].[Sports] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Disciplines_SportId]
    ON [dbo].[Disciplines]([SportId] ASC);

SET IDENTITY_INSERT [dbo].[Disciplines] ON
INSERT INTO [dbo].[Disciplines] ([Id], [Name], [SportId]) VALUES ('1','Track & Fields',1)
INSERT INTO [dbo].[Disciplines] ([Id], [Name], [SportId]) VALUES ('2','Road Running',1)
INSERT INTO [dbo].[Disciplines] ([Id], [Name], [SportId]) VALUES ('3','Race Walking',1)
INSERT INTO [dbo].[Disciplines] ([Id], [Name], [SportId]) VALUES ('4','Cross Country Running',1)
INSERT INTO [dbo].[Disciplines] ([Id], [Name], [SportId]) VALUES ('5','Mountain Running',1)
INSERT INTO [dbo].[Disciplines] ([Id], [Name], [SportId]) VALUES ('6','Trail Running',1)
INSERT INTO [dbo].[Disciplines] ([Id], [Name], [SportId]) VALUES ('7','Road Racing',2)
INSERT INTO [dbo].[Disciplines] ([Id], [Name], [SportId]) VALUES ('8','Track Cycling',2)
INSERT INTO [dbo].[Disciplines] ([Id], [Name], [SportId]) VALUES ('9','Cyclo Cross',2)
INSERT INTO [dbo].[Disciplines] ([Id], [Name], [SportId]) VALUES ('10','Mountain Bike Racing',2)
INSERT INTO [dbo].[Disciplines] ([Id], [Name], [SportId]) VALUES ('11','BMX racing',2)
INSERT INTO [dbo].[Disciplines] ([Id], [Name], [SportId]) VALUES ('12','Swimming',3)
INSERT INTO [dbo].[Disciplines] ([Id], [Name], [SportId]) VALUES ('13','Open Water Swimming',3)
INSERT INTO [dbo].[Disciplines] ([Id], [Name], [SportId]) VALUES ('14','Sled Dog Racing',5)
INSERT INTO [dbo].[Disciplines] ([Id], [Name], [SportId]) VALUES ('15','Alpine Skiing',4)
INSERT INTO [dbo].[Disciplines] ([Id], [Name], [SportId]) VALUES ('16','Nordic Skiing',4)
INSERT INTO [dbo].[Disciplines] ([Id], [Name], [SportId]) VALUES ('17','Freestyle Skiing',4)
INSERT INTO [dbo].[Disciplines] ([Id], [Name], [SportId]) VALUES ('18','Snowboarding',4)
INSERT INTO [dbo].[Disciplines] ([Id], [Name], [SportId]) VALUES ('19','Triathlon',6)
INSERT INTO [dbo].[Disciplines] ([Id], [Name], [SportId]) VALUES ('20','Biathlon',7)
SET IDENTITY_INSERT [dbo].[Disciplines] OFF

CREATE TABLE [dbo].[Courses] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [DisciplineId]     INT            NOT NULL,
    [Distance]         INT            NOT NULL,
    [ExternalCourseId] NVARCHAR (MAX) NULL,
    [Lap]              BIT            NOT NULL,
    [Name]             NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Courses_Disciplines_DisciplineId] FOREIGN KEY ([DisciplineId]) REFERENCES [dbo].[Disciplines] ([Id]) ON DELETE CASCADE
);

INSERT INTO [dbo].[Courses] ([Name], [DisciplineId], [Distance], [ExternalCourseId], [Lap]) VALUES ('Gleðibankahlaup 2017', '2', '5000', '1R501', 'FALSE')
INSERT INTO [dbo].[Courses] ([Name], [DisciplineId], [Distance], [ExternalCourseId], [Lap]) VALUES ('Gleðibankahlaup 2017', '2', '10000', '1R1001', 'FALSE')
INSERT INTO [dbo].[Courses] ([Name], [DisciplineId], [Distance], [ExternalCourseId], [Lap]) VALUES ('Vorhlaup 2017', '2', '1000', '1R101', 'TRUE')
INSERT INTO [dbo].[Courses] ([Name], [DisciplineId], [Distance], [ExternalCourseId], [Lap]) VALUES ('Vorhlaup 2018', '2', '1000', '1R102', 'TRUE')
INSERT INTO [dbo].[Courses] ([Name], [DisciplineId], [Distance], [ExternalCourseId], [Lap]) VALUES ('Laugavegurinn', '5', '50000', '1M5001', 'TRUE')
INSERT INTO [dbo].[Courses] ([Name], [DisciplineId], [Distance], [ExternalCourseId], [Lap]) VALUES ('Jökulílan', '7', '160000', '1C1001', 'TRUE')

CREATE TABLE [dbo].[Competitions] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Deleted]     BIT            NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [Email]       NVARCHAR (MAX) NULL,
    [Name]        NVARCHAR (MAX) NULL,
    [Phone]       NVARCHAR (MAX) NULL,
    [Sponsor]     NVARCHAR (MAX) NULL,
    [WebPage]     NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Competitions] PRIMARY KEY CLUSTERED ([Id] ASC)
);

INSERT INTO [dbo].[Competitions] ([Name], [Email], [Phone], [Sponsor], [WebPage], [Description], [Deleted]) VALUES ('Gleðibankahlaup', 'hlaup@gledibankinn.is', '5812345', 'Gleðibankinn ehf', 'gledibankinn.is', 'Gleðihlaup Gleðibankans', 'FALSE')
INSERT INTO [dbo].[Competitions] ([Name], [Email], [Phone], [Sponsor], [WebPage], [Description], [Deleted]) VALUES ('Laugavegshlaupið', 'fjallahlaup@laugavegur.is', '4441000', '', 'laugavegur.is', 'Hlaup frá Landmannalaugum í Þórsmörk', 'FALSE')
INSERT INTO [dbo].[Competitions] ([Name], [Email], [Phone], [Sponsor], [WebPage], [Description], [Deleted]) VALUES ('Jökulmílan', 'jokulmilan@hjolamenn.is', '5551000', '', 'hjolamenn.is', 'Götuhjólakeppni umhverfis Snæfellsjökul', 'FALSE')


CREATE TABLE [dbo].[ManagesCompetitions] (
    [UserId]        NVARCHAR (450) NOT NULL,
    [CompetitionId] INT            NOT NULL,
    [Role]          INT            NOT NULL,
    CONSTRAINT [PK_ManagesCompetitions] PRIMARY KEY CLUSTERED ([UserId] ASC, [CompetitionId] ASC),
    CONSTRAINT [AK_ManagesCompetitions_CompetitionId_UserId] UNIQUE NONCLUSTERED ([CompetitionId] ASC, [UserId] ASC),
    CONSTRAINT [FK_ManagesCompetitions_Competitions_CompetitionId] FOREIGN KEY ([CompetitionId]) REFERENCES [dbo].[Competitions] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ManagesCompetitions_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[CompetitionInstances] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [CompetitionId] INT            NOT NULL,
    [Name]          NVARCHAR (MAX) NULL,
    [CountryId]     INT            NOT NULL,
    [DateFrom]      DATETIME2 (7)  NOT NULL,
    [DateTo]        DATETIME2 (7)  NOT NULL,
    [Deleted]       BIT            NOT NULL,
    [Location]      NVARCHAR (MAX) NULL,
    [Status]        INT            NOT NULL,
    CONSTRAINT [PK_CompetitionInstances] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CompetitionInstances_Competitions_CompetitionId] FOREIGN KEY ([CompetitionId]) REFERENCES [dbo].[Competitions] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CompetitionInstances_Countries_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries] ([Id]) ON DELETE CASCADE
);

GO
CREATE NONCLUSTERED INDEX [IX_CompetitionInstances_CompetitionId]
    ON [dbo].[CompetitionInstances]([CompetitionId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CompetitionInstances_CountryId]
    ON [dbo].[CompetitionInstances]([CountryId] ASC);

INSERT INTO [dbo].[CompetitionInstances] ([CompetitionId], [Name], [CountryId], [DateFrom], [DateTo], [Deleted], [Location], [Status]) VALUES ('1', 'Gleiðbankahlaup 2017', '352', '2/6/2017 12:00:00', '2/6/2017 13:30:00', 'FALSE', 'Reykjavík', '4')
INSERT INTO [dbo].[CompetitionInstances] ([CompetitionId], [Name], [CountryId], [DateFrom], [DateTo], [Deleted], [Location], [Status]) VALUES ('1', 'Gleiðbankahlaup 2018', '352', '4/6/2018 12:00:00', '4/6/2018 13:30:00', 'FALSE', 'Reykjavík', '0')
INSERT INTO [dbo].[CompetitionInstances] ([CompetitionId], [Name], [CountryId], [DateFrom], [DateTo], [Deleted], [Location], [Status]) VALUES ('2', 'Laugavegshlaupið 2015', '352', '6/24/2015 8:00:00', '6/24/2015 16:00:00', 'TRUE', 'Landmannalaugar', '0')
INSERT INTO [dbo].[CompetitionInstances] ([CompetitionId], [Name], [CountryId], [DateFrom], [DateTo], [Deleted], [Location], [Status]) VALUES ('2', 'Laugavegshlaupið 2015', '352', '6/24/2015 8:00:00', '6/24/2015 16:00:00', 'FALSE', 'Landmannalaugar', '0')
INSERT INTO [dbo].[CompetitionInstances] ([CompetitionId], [Name], [CountryId], [DateFrom], [DateTo], [Deleted], [Location], [Status]) VALUES ('2', 'Laugavegshlaupið 2016', '352', '6/24/2016 8:00:00', '6/24/2016 16:00:00', 'FALSE', 'Landmannalaugar', '0')
INSERT INTO [dbo].[CompetitionInstances] ([CompetitionId], [Name], [CountryId], [DateFrom], [DateTo], [Deleted], [Location], [Status]) VALUES ('2', 'Laugavegshlaupið 2017', '352', '6/24/2017 8:00:00', '6/24/2017 16:00:00', 'FALSE', 'Landmannalaugar', '0')
INSERT INTO [dbo].[CompetitionInstances] ([CompetitionId], [Name], [CountryId], [DateFrom], [DateTo], [Deleted], [Location], [Status]) VALUES ('3', 'Jökulmílan 2017', '352', '6/5/2017 8:00:00', '6/5/2018 16:00:00', 'FALSE', 'Snæfellsnes', '4')
INSERT INTO [dbo].[CompetitionInstances] ([CompetitionId], [Name], [CountryId], [DateFrom], [DateTo], [Deleted], [Location], [Status]) VALUES ('3', 'Jökulmílan 2018', '352', '6/5/2018 8:00:00', '6/5/2018 16:00:00', 'FALSE', 'Snæfellsnes', '0')

CREATE TABLE [dbo].[Events] (
    [Id]                    INT            IDENTITY (1, 1) NOT NULL,
    [ActiveChip]            BIT            NOT NULL,
    [CompetitionInstanceId] INT            NOT NULL,
    [CourseId]              INT            NOT NULL,
    [DateFrom]              DATETIME2 (7)  NOT NULL,
    [DateTo]                DATETIME2 (7)  NOT NULL,
    [DisciplineId]          INT            NOT NULL,
    [DistanceOffset]        INT            NOT NULL,
    [Gender]                INT            NOT NULL,
    [Laps]                  INT            NOT NULL,
    [Name]                  NVARCHAR (MAX) NULL,
    [Splits]                INT            NOT NULL,
    [StartInterval]         INT            NOT NULL,
    [Deleted]               BIT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Events_CompetitionInstances_CompetitionInstanceId] FOREIGN KEY ([CompetitionInstanceId]) REFERENCES [dbo].[CompetitionInstances] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Events_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Courses] ([Id]),
    CONSTRAINT [FK_Events_Disciplines_DisciplineId] FOREIGN KEY ([DisciplineId]) REFERENCES [dbo].[Disciplines] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Events_CompetitionInstanceId]
    ON [dbo].[Events]([CompetitionInstanceId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Events_DisciplineId]
    ON [dbo].[Events]([DisciplineId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Events_CourseId]
    ON [dbo].[Events]([CourseId] ASC);

INSERT INTO [dbo].[Events] ([ActiveChip], [CompetitionInstanceId], [CourseId], [DateFrom], [DateTo], [DisciplineId], [DistanceOffset], [Gender], [Laps], [Name], [Splits], [StartInterval], [Deleted]) VALUES ('False', '1', '1', '2/6/2017 12:00:00', '2/6/2017 12:45:00', '2', '', '2', '', '5 km Götuhlaup', '', '', 'FALSE')
INSERT INTO [dbo].[Events] ([ActiveChip], [CompetitionInstanceId], [CourseId], [DateFrom], [DateTo], [DisciplineId], [DistanceOffset], [Gender], [Laps], [Name], [Splits], [StartInterval], [Deleted]) VALUES ('False', '1', '2', '2/6/2017 12:10:00', '2/6/2017 13:30:00', '2', '', '2', '', '10 km Götuhlaup', '', '', 'FALSE')
INSERT INTO [dbo].[Events] ([ActiveChip], [CompetitionInstanceId], [CourseId], [DateFrom], [DateTo], [DisciplineId], [DistanceOffset], [Gender], [Laps], [Name], [Splits], [StartInterval], [Deleted]) VALUES ('False', '2', '2', '4/6/2018 12:00:00', '4/6/2017 13:30:00', '2', '', '1', '', '10 km Götuhlaup karla', '', '', 'FALSE')
INSERT INTO [dbo].[Events] ([ActiveChip], [CompetitionInstanceId], [CourseId], [DateFrom], [DateTo], [DisciplineId], [DistanceOffset], [Gender], [Laps], [Name], [Splits], [StartInterval], [Deleted]) VALUES ('False', '2', '2', '4/6/2018 12:00:00', '4/6/2017 13:30:00', '2', '', '0', '', '10 km Götuhlaup kvenna', '', '', 'FALSE')
INSERT INTO [dbo].[Events] ([ActiveChip], [CompetitionInstanceId], [CourseId], [DateFrom], [DateTo], [DisciplineId], [DistanceOffset], [Gender], [Laps], [Name], [Splits], [StartInterval], [Deleted]) VALUES ('False', '2', '2', '4/6/2018 12:00:00', '4/6/2017 13:0:00', '2', '', '2', '', '5 km Götuhlaup', '', '', 'FALSE')
INSERT INTO [dbo].[Events] ([ActiveChip], [CompetitionInstanceId], [CourseId], [DateFrom], [DateTo], [DisciplineId], [DistanceOffset], [Gender], [Laps], [Name], [Splits], [StartInterval], [Deleted]) VALUES ('False', '3', '5', '6/24/2015 8:00:00', '6/24/2015 16:00:00', '5', '', '2', '', 'Laugavegshlaupið', '3', '', 'FALSE')
INSERT INTO [dbo].[Events] ([ActiveChip], [CompetitionInstanceId], [CourseId], [DateFrom], [DateTo], [DisciplineId], [DistanceOffset], [Gender], [Laps], [Name], [Splits], [StartInterval], [Deleted]) VALUES ('False', '4', '5', '6/24/2016 8:00:00', '6/24/2016 16:00:00', '5', '', '2', '', 'Laugavegshlaupið', '3', '', 'FALSE')
INSERT INTO [dbo].[Events] ([ActiveChip], [CompetitionInstanceId], [CourseId], [DateFrom], [DateTo], [DisciplineId], [DistanceOffset], [Gender], [Laps], [Name], [Splits], [StartInterval], [Deleted]) VALUES ('False', '5', '5', '6/24/2017 8:00:00', '6/24/2017 16:00:00', '5', '', '2', '', 'Laugavegshlaupið', '3', '', 'FALSE')
INSERT INTO [dbo].[Events] ([ActiveChip], [CompetitionInstanceId], [CourseId], [DateFrom], [DateTo], [DisciplineId], [DistanceOffset], [Gender], [Laps], [Name], [Splits], [StartInterval], [Deleted]) VALUES ('False', '6', '5', '6/24/2018 8:00:00', '6/24/2018 16:00:00', '5', '', '2', '', 'Laugavegshlaupið', '3', '', 'FALSE')
INSERT INTO [dbo].[Events] ([ActiveChip], [CompetitionInstanceId], [CourseId], [DateFrom], [DateTo], [DisciplineId], [DistanceOffset], [Gender], [Laps], [Name], [Splits], [StartInterval], [Deleted]) VALUES ('TRUE', '7', '6', '6/5/2017 8:00:00', '6/5/2017 16:00:00', '7', '', '2', '', 'Heil Jökulmíla', '3', '', 'FALSE')
INSERT INTO [dbo].[Events] ([ActiveChip], [CompetitionInstanceId], [CourseId], [DateFrom], [DateTo], [DisciplineId], [DistanceOffset], [Gender], [Laps], [Name], [Splits], [StartInterval], [Deleted]) VALUES ('TRUE', '7', '6', '6/5/2017 8:00:00', '6/5/2017 12:00:00', '7', '-80000', '2', '', 'Hálf Jökulmíla', '3', '', 'FALSE')
INSERT INTO [dbo].[Events] ([ActiveChip], [CompetitionInstanceId], [CourseId], [DateFrom], [DateTo], [DisciplineId], [DistanceOffset], [Gender], [Laps], [Name], [Splits], [StartInterval], [Deleted]) VALUES ('TRUE', '8', '6', '6/5/2018 8:00:00', '6/5/2018 16:00:00', '7', '', '2', '', 'Heil Jökulmíla', '3', '', 'FALSE')
INSERT INTO [dbo].[Events] ([ActiveChip], [CompetitionInstanceId], [CourseId], [DateFrom], [DateTo], [DisciplineId], [DistanceOffset], [Gender], [Laps], [Name], [Splits], [StartInterval], [Deleted]) VALUES ('TRUE', '8', '6', '6/5/2018 8:00:00', '6/5/2018 12:00:00', '7', '-80000', '2', '', 'Hálf Jökulmíla', '3', '300', 'FALSE')

CREATE TABLE [dbo].[Heats] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [Deleted]    BIT NOT NULL,
    [EventId]    INT NOT NULL,
    [HeatNumber] INT NOT NULL,
    CONSTRAINT [PK_Heats] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Heats_Events_EventId] FOREIGN KEY ([EventId]) REFERENCES [dbo].[Events] ([Id]) ON DELETE CASCADE
);

INSERT INTO [dbo].[Heats] ([Deleted], [EventId], [HeatNumber]) VALUES ('FALSE', '1', '0')
INSERT INTO [dbo].[Heats] ([Deleted], [EventId], [HeatNumber]) VALUES ('FALSE', '2', '0')
INSERT INTO [dbo].[Heats] ([Deleted], [EventId], [HeatNumber]) VALUES ('FALSE', '3', '0')
INSERT INTO [dbo].[Heats] ([Deleted], [EventId], [HeatNumber]) VALUES ('FALSE', '4', '0')
INSERT INTO [dbo].[Heats] ([Deleted], [EventId], [HeatNumber]) VALUES ('FALSE', '5', '0')
INSERT INTO [dbo].[Heats] ([Deleted], [EventId], [HeatNumber]) VALUES ('FALSE', '6', '0')
INSERT INTO [dbo].[Heats] ([Deleted], [EventId], [HeatNumber]) VALUES ('FALSE', '7', '0')
INSERT INTO [dbo].[Heats] ([Deleted], [EventId], [HeatNumber]) VALUES ('FALSE', '8', '0')
INSERT INTO [dbo].[Heats] ([Deleted], [EventId], [HeatNumber]) VALUES ('FALSE', '9', '0')
INSERT INTO [dbo].[Heats] ([Deleted], [EventId], [HeatNumber]) VALUES ('FALSE', '10', '0')
INSERT INTO [dbo].[Heats] ([Deleted], [EventId], [HeatNumber]) VALUES ('FALSE', '11', '0')
INSERT INTO [dbo].[Heats] ([Deleted], [EventId], [HeatNumber]) VALUES ('FALSE', '12', '0')
INSERT INTO [dbo].[Heats] ([Deleted], [EventId], [HeatNumber]) VALUES ('FALSE', '13', '0')
INSERT INTO [dbo].[Heats] ([Deleted], [EventId], [HeatNumber]) VALUES ('FALSE', '13', '1')
INSERT INTO [dbo].[Heats] ([Deleted], [EventId], [HeatNumber]) VALUES ('FALSE', '13', '2')


CREATE TABLE [dbo].[ContestantsInHeats] (
    [UserId]   NVARCHAR (450) NOT NULL,
    [HeatId]   INT            NOT NULL,
    [Bib]      INT            NOT NULL,
    [Modified] DATETIME2 (7)  NOT NULL,
    [Team]     NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_ContestantsInHeats] PRIMARY KEY CLUSTERED ([UserId] ASC, [HeatId] ASC),
    CONSTRAINT [AK_ContestantsInHeats_HeatId_UserId] UNIQUE NONCLUSTERED ([HeatId] ASC, [UserId] ASC),
    CONSTRAINT [FK_ContestantsInHeats_Heats_HeatId] FOREIGN KEY ([HeatId]) REFERENCES [dbo].[Heats] ([Id]),
    CONSTRAINT [FK_ContestantsInHeats_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);