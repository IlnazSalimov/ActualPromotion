
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/20/2014 14:17:32
-- Generated from EDMX file: C:\Users\Ильназ\Desktop\Projects\Brio\Brio\Models\BrioEntityModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [actual98_brioDb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Articles__Author__1ED998B2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Articles] DROP CONSTRAINT [FK__Articles__Author__1ED998B2];
GO
IF OBJECT_ID(N'[dbo].[FK__Articles__Compan__412EB0B6]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Articles] DROP CONSTRAINT [FK__Articles__Compan__412EB0B6];
GO
IF OBJECT_ID(N'[dbo].[FK__Documents__Compa__3F466844]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Documents] DROP CONSTRAINT [FK__Documents__Compa__3F466844];
GO
IF OBJECT_ID(N'[dbo].[FK__Feedbacks__Compa__440B1D61]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Feedbacks] DROP CONSTRAINT [FK__Feedbacks__Compa__440B1D61];
GO
IF OBJECT_ID(N'[dbo].[FK__PriceList__Compa__5441852A]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PriceLists] DROP CONSTRAINT [FK__PriceList__Compa__5441852A];
GO
IF OBJECT_ID(N'[dbo].[FK__Reviews__Company__46E78A0C]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reviews] DROP CONSTRAINT [FK__Reviews__Company__46E78A0C];
GO
IF OBJECT_ID(N'[dbo].[FK__Users__RoleId__1BFD2C07]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK__Users__RoleId__1BFD2C07];
GO
IF OBJECT_ID(N'[dbo].[FK__Divisions__Compa__5CD6CB2B]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Divisions] DROP CONSTRAINT [FK__Divisions__Compa__5CD6CB2B];
GO
IF OBJECT_ID(N'[dbo].[FK__News__CompanyId__66603565]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[News] DROP CONSTRAINT [FK__News__CompanyId__66603565];
GO
IF OBJECT_ID(N'[dbo].[FK__Projects__Compan__6B24EA82]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [FK__Projects__Compan__6B24EA82];
GO
IF OBJECT_ID(N'[dbo].[FK__InfoCards__Divis__6383C8BA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InfoCards] DROP CONSTRAINT [FK__InfoCards__Divis__6383C8BA];
GO
IF OBJECT_ID(N'[dbo].[FK__News__DivisionId__6754599E]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[News] DROP CONSTRAINT [FK__News__DivisionId__6754599E];
GO
IF OBJECT_ID(N'[dbo].[FK__InfoCards__UserI__628FA481]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InfoCards] DROP CONSTRAINT [FK__InfoCards__UserI__628FA481];
GO
IF OBJECT_ID(N'[dbo].[FK__News__AuthorUser__68487DD7]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[News] DROP CONSTRAINT [FK__News__AuthorUser__68487DD7];
GO
IF OBJECT_ID(N'[dbo].[FK__Projects__Respon__6C190EBB]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [FK__Projects__Respon__6C190EBB];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Articles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Articles];
GO
IF OBJECT_ID(N'[dbo].[Companies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Companies];
GO
IF OBJECT_ID(N'[dbo].[Documents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Documents];
GO
IF OBJECT_ID(N'[dbo].[Feedbacks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Feedbacks];
GO
IF OBJECT_ID(N'[dbo].[PriceLists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PriceLists];
GO
IF OBJECT_ID(N'[dbo].[Reviews]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Reviews];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Divisions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Divisions];
GO
IF OBJECT_ID(N'[dbo].[InfoCards]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InfoCards];
GO
IF OBJECT_ID(N'[dbo].[News]', 'U') IS NOT NULL
    DROP TABLE [dbo].[News];
GO
IF OBJECT_ID(N'[dbo].[Projects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Projects];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Articles'
CREATE TABLE [dbo].[Articles] (
    [ArticleID] int IDENTITY(1,1) NOT NULL,
    [Title] varchar(127)  NOT NULL,
    [Text] varchar(max)  NOT NULL,
    [Page] varchar(127)  NULL,
    [Author] int  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [CompanyId] int  NOT NULL
);
GO

-- Creating table 'Companies'
CREATE TABLE [dbo].[Companies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CompanyName] varchar(127)  NOT NULL,
    [Adress] varchar(255)  NOT NULL,
    [Phone] varchar(127)  NOT NULL,
    [Phone2] varchar(127)  NULL,
    [Email] varchar(127)  NULL,
    [Postcode] varchar(127)  NULL,
    [POBox] int  NULL,
    [Phone3] varchar(127)  NULL
);
GO

-- Creating table 'Documents'
CREATE TABLE [dbo].[Documents] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CompanyId] int  NOT NULL,
    [DocumentPath] varchar(127)  NOT NULL,
    [DocumentTitle] varchar(255)  NOT NULL,
    [UploadDate] datetime  NOT NULL
);
GO

-- Creating table 'Feedbacks'
CREATE TABLE [dbo].[Feedbacks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(127)  NOT NULL,
    [Email] varchar(127)  NOT NULL,
    [Phone] varchar(127)  NOT NULL,
    [Message] varchar(max)  NOT NULL,
    [CompanyId] int  NOT NULL
);
GO

-- Creating table 'PriceLists'
CREATE TABLE [dbo].[PriceLists] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CompanyId] int  NOT NULL,
    [Path] varchar(255)  NOT NULL,
    [Title] varchar(255)  NOT NULL,
    [UploadDate] datetime  NOT NULL
);
GO

-- Creating table 'Reviews'
CREATE TABLE [dbo].[Reviews] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Title] varchar(255)  NOT NULL,
    [ReviewMessage] varchar(max)  NOT NULL,
    [AutorPosition] varchar(127)  NOT NULL,
    [AuthorCompany] varchar(255)  NOT NULL,
    [AuthorPhoto] varchar(255)  NOT NULL,
    [CompanyId] int  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RoleName] varchar(127)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Email] varchar(127)  NOT NULL,
    [Password] varchar(127)  NOT NULL,
    [RoleId] int  NOT NULL
);
GO

-- Creating table 'Divisions'
CREATE TABLE [dbo].[Divisions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(255)  NOT NULL,
    [Head] varchar(255)  NOT NULL,
    [CompanyId] int  NOT NULL
);
GO

-- Creating table 'InfoCards'
CREATE TABLE [dbo].[InfoCards] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [DivisionId] int  NOT NULL,
    [Post] varchar(255)  NOT NULL,
    [GetJobDate] datetime  NOT NULL,
    [BirthDay] datetime  NOT NULL,
    [Name] varchar(255)  NOT NULL,
    [Surname] varchar(255)  NOT NULL,
    [Patronymic] varchar(255)  NOT NULL,
    [Adress] varchar(255)  NOT NULL,
    [Phone] varchar(255)  NOT NULL,
    [Email] varchar(255)  NOT NULL
);
GO

-- Creating table 'News'
CREATE TABLE [dbo].[News] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CompanyId] int  NOT NULL,
    [DivisionId] int  NOT NULL,
    [AuthorUserId] int  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [Tile] varchar(255)  NOT NULL,
    [Text] varchar(max)  NOT NULL
);
GO

-- Creating table 'Projects'
CREATE TABLE [dbo].[Projects] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CompanyId] int  NOT NULL,
    [Tile] varchar(255)  NOT NULL,
    [ResponsibleUserId] int  NULL,
    [Description] varchar(511)  NOT NULL,
    [StartDate] datetime  NULL,
    [EndDate] datetime  NULL,
    [CreateDate] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ArticleID] in table 'Articles'
ALTER TABLE [dbo].[Articles]
ADD CONSTRAINT [PK_Articles]
    PRIMARY KEY CLUSTERED ([ArticleID] ASC);
GO

-- Creating primary key on [Id] in table 'Companies'
ALTER TABLE [dbo].[Companies]
ADD CONSTRAINT [PK_Companies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Documents'
ALTER TABLE [dbo].[Documents]
ADD CONSTRAINT [PK_Documents]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Feedbacks'
ALTER TABLE [dbo].[Feedbacks]
ADD CONSTRAINT [PK_Feedbacks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PriceLists'
ALTER TABLE [dbo].[PriceLists]
ADD CONSTRAINT [PK_PriceLists]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Reviews'
ALTER TABLE [dbo].[Reviews]
ADD CONSTRAINT [PK_Reviews]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Divisions'
ALTER TABLE [dbo].[Divisions]
ADD CONSTRAINT [PK_Divisions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'InfoCards'
ALTER TABLE [dbo].[InfoCards]
ADD CONSTRAINT [PK_InfoCards]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'News'
ALTER TABLE [dbo].[News]
ADD CONSTRAINT [PK_News]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [PK_Projects]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Author] in table 'Articles'
ALTER TABLE [dbo].[Articles]
ADD CONSTRAINT [FK__Articles__Author__1ED998B2]
    FOREIGN KEY ([Author])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Articles__Author__1ED998B2'
CREATE INDEX [IX_FK__Articles__Author__1ED998B2]
ON [dbo].[Articles]
    ([Author]);
GO

-- Creating foreign key on [CompanyId] in table 'Articles'
ALTER TABLE [dbo].[Articles]
ADD CONSTRAINT [FK__Articles__Compan__412EB0B6]
    FOREIGN KEY ([CompanyId])
    REFERENCES [dbo].[Companies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Articles__Compan__412EB0B6'
CREATE INDEX [IX_FK__Articles__Compan__412EB0B6]
ON [dbo].[Articles]
    ([CompanyId]);
GO

-- Creating foreign key on [CompanyId] in table 'Documents'
ALTER TABLE [dbo].[Documents]
ADD CONSTRAINT [FK__Documents__Compa__3F466844]
    FOREIGN KEY ([CompanyId])
    REFERENCES [dbo].[Companies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Documents__Compa__3F466844'
CREATE INDEX [IX_FK__Documents__Compa__3F466844]
ON [dbo].[Documents]
    ([CompanyId]);
GO

-- Creating foreign key on [CompanyId] in table 'Feedbacks'
ALTER TABLE [dbo].[Feedbacks]
ADD CONSTRAINT [FK__Feedbacks__Compa__440B1D61]
    FOREIGN KEY ([CompanyId])
    REFERENCES [dbo].[Companies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Feedbacks__Compa__440B1D61'
CREATE INDEX [IX_FK__Feedbacks__Compa__440B1D61]
ON [dbo].[Feedbacks]
    ([CompanyId]);
GO

-- Creating foreign key on [CompanyId] in table 'PriceLists'
ALTER TABLE [dbo].[PriceLists]
ADD CONSTRAINT [FK__PriceList__Compa__5441852A]
    FOREIGN KEY ([CompanyId])
    REFERENCES [dbo].[Companies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__PriceList__Compa__5441852A'
CREATE INDEX [IX_FK__PriceList__Compa__5441852A]
ON [dbo].[PriceLists]
    ([CompanyId]);
GO

-- Creating foreign key on [CompanyId] in table 'Reviews'
ALTER TABLE [dbo].[Reviews]
ADD CONSTRAINT [FK__Reviews__Company__46E78A0C]
    FOREIGN KEY ([CompanyId])
    REFERENCES [dbo].[Companies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Reviews__Company__46E78A0C'
CREATE INDEX [IX_FK__Reviews__Company__46E78A0C]
ON [dbo].[Reviews]
    ([CompanyId]);
GO

-- Creating foreign key on [RoleId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK__Users__RoleId__1BFD2C07]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Users__RoleId__1BFD2C07'
CREATE INDEX [IX_FK__Users__RoleId__1BFD2C07]
ON [dbo].[Users]
    ([RoleId]);
GO

-- Creating foreign key on [CompanyId] in table 'Divisions'
ALTER TABLE [dbo].[Divisions]
ADD CONSTRAINT [FK__Divisions__Compa__5CD6CB2B]
    FOREIGN KEY ([CompanyId])
    REFERENCES [dbo].[Companies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Divisions__Compa__5CD6CB2B'
CREATE INDEX [IX_FK__Divisions__Compa__5CD6CB2B]
ON [dbo].[Divisions]
    ([CompanyId]);
GO

-- Creating foreign key on [CompanyId] in table 'News'
ALTER TABLE [dbo].[News]
ADD CONSTRAINT [FK__News__CompanyId__66603565]
    FOREIGN KEY ([CompanyId])
    REFERENCES [dbo].[Companies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__News__CompanyId__66603565'
CREATE INDEX [IX_FK__News__CompanyId__66603565]
ON [dbo].[News]
    ([CompanyId]);
GO

-- Creating foreign key on [CompanyId] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [FK__Projects__Compan__6B24EA82]
    FOREIGN KEY ([CompanyId])
    REFERENCES [dbo].[Companies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Projects__Compan__6B24EA82'
CREATE INDEX [IX_FK__Projects__Compan__6B24EA82]
ON [dbo].[Projects]
    ([CompanyId]);
GO

-- Creating foreign key on [DivisionId] in table 'InfoCards'
ALTER TABLE [dbo].[InfoCards]
ADD CONSTRAINT [FK__InfoCards__Divis__6383C8BA]
    FOREIGN KEY ([DivisionId])
    REFERENCES [dbo].[Divisions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__InfoCards__Divis__6383C8BA'
CREATE INDEX [IX_FK__InfoCards__Divis__6383C8BA]
ON [dbo].[InfoCards]
    ([DivisionId]);
GO

-- Creating foreign key on [DivisionId] in table 'News'
ALTER TABLE [dbo].[News]
ADD CONSTRAINT [FK__News__DivisionId__6754599E]
    FOREIGN KEY ([DivisionId])
    REFERENCES [dbo].[Divisions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__News__DivisionId__6754599E'
CREATE INDEX [IX_FK__News__DivisionId__6754599E]
ON [dbo].[News]
    ([DivisionId]);
GO

-- Creating foreign key on [UserId] in table 'InfoCards'
ALTER TABLE [dbo].[InfoCards]
ADD CONSTRAINT [FK__InfoCards__UserI__628FA481]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__InfoCards__UserI__628FA481'
CREATE INDEX [IX_FK__InfoCards__UserI__628FA481]
ON [dbo].[InfoCards]
    ([UserId]);
GO

-- Creating foreign key on [AuthorUserId] in table 'News'
ALTER TABLE [dbo].[News]
ADD CONSTRAINT [FK__News__AuthorUser__68487DD7]
    FOREIGN KEY ([AuthorUserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__News__AuthorUser__68487DD7'
CREATE INDEX [IX_FK__News__AuthorUser__68487DD7]
ON [dbo].[News]
    ([AuthorUserId]);
GO

-- Creating foreign key on [ResponsibleUserId] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [FK__Projects__Respon__6C190EBB]
    FOREIGN KEY ([ResponsibleUserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Projects__Respon__6C190EBB'
CREATE INDEX [IX_FK__Projects__Respon__6C190EBB]
ON [dbo].[Projects]
    ([ResponsibleUserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------