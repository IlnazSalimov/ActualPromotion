CREATE TABLE Articles(
   ArticleID   INT IDENTITY(1,1)                NOT NULL,
   Title VARCHAR (127)     NOT NULL,
   Text  TEXT              NOT NULL,
   [Page] VARCHAR(127)     NULL,      
   PRIMARY KEY (ArticleID)
);

CREATE TABLE Roles(
   Id   INT IDENTITY(1,1)                NOT NULL,
   RoleName VARCHAR(127)   NOT NULL,   
   PRIMARY KEY (Id)
);

CREATE TABLE Users(
   [Id]   INT IDENTITY(1,1)                NOT NULL,
   [Email] VARCHAR (127)         NOT NULL,
   [Password] VARCHAR (127)      NOT NULL,
   [RoleId] INT                  NOT NULL,
   PRIMARY KEY (Id),
   FOREIGN KEY (RoleId) REFERENCES Roles (Id)
);

CREATE TABLE [dbo].[Feedbacks]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] VARCHAR(127) NOT NULL,
	[E-mail] VARCHAR(127) NOT NULL,
	[Phone] VARCHAR(127) NOT NULL,
	[Message] TEXT NOT NULL
)

CREATE TABLE [dbo].[Company]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[CompanyName] VARCHAR (127) NOT NULL,
    [Adress]      VARCHAR (255) NOT NULL,
    [Phone]       VARCHAR(127)  NOT NULL,
	[Phone2]       VARCHAR(127) NULL,
    [Email]       VARCHAR (127) NULL,
    [Postcode]    VARCHAR (127) NULL,
	[POBox] INT NULL
)

CREATE TABLE [dbo].[PriceLists]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[CompanyId] INT NOT NULL,
    [PriceList]      VARCHAR (255) NOT NULL,
	FOREIGN KEY (CompanyId) REFERENCES Company(Id)
)