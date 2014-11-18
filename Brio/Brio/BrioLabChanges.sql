ALTER DATABASE BrioDataBase COLLATE Cyrillic_General_CI_AS
GO

CREATE TABLE [dbo].[Roles](
   Id   INT IDENTITY(1,1)                NOT NULL,
   RoleName NVARCHAR(127)   NOT NULL,   
   PRIMARY KEY (Id)
)

GO

CREATE TABLE [dbo].[Users](
   [Id]   INT IDENTITY(1,1)       NOT NULL,
   [Email] NVARCHAR (127)         NOT NULL,
   [Password] NVARCHAR (127)      NOT NULL,
   [RoleId] INT                  NOT NULL,
   [CompanyId] INT          NOT NULL,
   PRIMARY KEY (Id),
   FOREIGN KEY (RoleId) REFERENCES Roles (Id),
   FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([Id])
);

GO

CREATE TABLE [dbo].[Company]
(
	[Id] INT  NOT NULL PRIMARY KEY,
	[CompanyName] NVARCHAR (127) NOT NULL,
    [Adress]      NVARCHAR (255) NOT NULL,
    [Phone]       NVARCHAR(127)  NOT NULL,
	[Phone2]      NVARCHAR(127) NULL,
	[Phone3]      NVARCHAR (127) NULL,
    [Email]       NVARCHAR (127) NULL,
    [Postcode]    NVARCHAR (127) NULL,
	[POBox] INT NULL
)

GO


CREATE TABLE [dbo].[Articles] (
    [ArticleID]  INT           IDENTITY (1, 1) NOT NULL,
    [Title]      NVARCHAR (127) NOT NULL,
    [Text]       NTEXT          NOT NULL,
    [Page]       NVARCHAR (127) NULL,
    [Author]     INT           NOT NULL,
    [CreateDate] DATE          NOT NULL,
	[CompanyId] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([ArticleID] ASC),
    FOREIGN KEY ([Author]) REFERENCES [dbo].[Users] ([Id]),
	FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([Id])
);

GO


CREATE TABLE [dbo].[Feedbacks] (
    [Id]      INT           IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (127) NOT NULL,
    [Email]   NVARCHAR (127) NOT NULL,
    [Phone]   NVARCHAR (127) NOT NULL,
    [Message] NTEXT          NOT NULL,
	[CompanyId] INT          NOT NULL DEFAULT 1,
    PRIMARY KEY CLUSTERED ([Id] ASC),
	FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([Id])
);

GO

CREATE TABLE [dbo].[PriceLists] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [CompanyId] INT           NOT NULL,
    [Path] NVARCHAR (255) NOT NULL,
	[Title] NVARCHAR(255) NOT NULL,
	[UploadDate] DATETIME NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([Id])
);

GO

CREATE TABLE [dbo].[Documents] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [CompanyId]     INT           NOT NULL,
    [DocumentPath]  VARCHAR (127) NOT NULL,
    [DocumentTitle] VARCHAR (255) DEFAULT ('untitle') NOT NULL,
    [UploadDate]    DATETIME      DEFAULT ('11/09/2014 00:00:0000') NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([Id])
);

GO

CREATE TABLE [dbo].[Reviews]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Date] DATETIME NOT NULL,
	[Title] NVARCHAR(255) NOT NULL,
	[ReviewMessage] NTEXT NOT NULL,
	[AutorPosition] NVARCHAR(127) NOT NULL,
	[AuthorCompany] NVARCHAR(255) NOT NULL,
	[AuthorPhoto] NVARCHAR(255) NOT NULL,
	[CompanyId] INT NOT NULL,
	FOREIGN KEY (CompanyId) REFERENCES Company(Id)
)

GO

CREATE TABLE [dbo].[Divisions]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(255) NOT NULL,
	[Head] NVARCHAR(255) NOT NULL,
	[CompanyId] INT NOT NULL,
	FOREIGN KEY (CompanyId) REFERENCES Company(Id)
)

CREATE TABLE [dbo].[InfoCards] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [UserId]     INT           NOT NULL,
    [DivisionId] INT           NULL,
    [Post]       VARCHAR (255) NULL,
    [GetJobDate] DATE          NULL,
    [BirthDay]   DATE          NULL,
    [Name]       VARCHAR (255) NOT NULL,
    [Surname]    VARCHAR (255) NOT NULL,
    [Patronymic] VARCHAR (255) NULL,
    [Adress]     VARCHAR (255) NULL,
    [Phone]      VARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]),
    FOREIGN KEY ([DivisionId]) REFERENCES [dbo].[Divisions] ([Id])
);

CREATE TABLE [dbo].[News]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[CompanyId] INT NOT NULL,
	[DivisionId] INT NOT NULL,
	[AuthorUserId] INT NOT NULL,
	[CreateDate] DATE NOT NULL,
	[Tile] NVARCHAR(255) NOT NULL,
	[Text] NTEXT NOT NULL,
	FOREIGN KEY (CompanyId) REFERENCES Company(Id),
	FOREIGN KEY ([DivisionId]) REFERENCES [dbo].[Divisions] ([Id]),
	FOREIGN KEY ([AuthorUserId]) REFERENCES Users(Id)
)


CREATE TABLE [dbo].[Projects]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[CompanyId] INT NOT NULL,
	[Tile] NVARCHAR(255) NOT NULL,
	[ResponsibleUserId] INT NULL,
	[Description] NVARCHAR(511) NOT NULL,
	[StartDate] DATE NULL,
	[EndDate] DATE NULL,
	[CreateDate] DATE NOT NULL,
	FOREIGN KEY (CompanyId) REFERENCES Company(Id),
	FOREIGN KEY ([ResponsibleUserId]) REFERENCES Users(Id)
)

INSERT INTO Roles(RoleName) VALUES ('Admin');
GO

INSERT INTO Users(Email, Password, RoleId) VALUES ('salimov.iln@gmail.com', '5112983', 1);
GO
