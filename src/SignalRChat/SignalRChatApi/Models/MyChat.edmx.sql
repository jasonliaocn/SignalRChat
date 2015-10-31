
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/14/2015 11:35:01
-- Generated from EDMX file: D:\VS13Project\SignalRChat\SignalRChatApi\Models\MyChat.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MyChat];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__ChatConte__Sessi__4277DAAA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ChatContent] DROP CONSTRAINT [FK__ChatConte__Sessi__4277DAAA];
GO
IF OBJECT_ID(N'[dbo].[FK__ChatSessi__Sessi__4A18FC72]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ChatSessionJoinner] DROP CONSTRAINT [FK__ChatSessi__Sessi__4A18FC72];
GO
IF OBJECT_ID(N'[dbo].[FK__ChatSessi__Start__3F9B6DFF]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ChatSession] DROP CONSTRAINT [FK__ChatSessi__Start__3F9B6DFF];
GO
IF OBJECT_ID(N'[dbo].[FK__ChatSessi__UserI__4B0D20AB]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ChatSessionJoinner] DROP CONSTRAINT [FK__ChatSessi__UserI__4B0D20AB];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ChatContent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ChatContent];
GO
IF OBJECT_ID(N'[dbo].[ChatSession]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ChatSession];
GO
IF OBJECT_ID(N'[dbo].[ChatSessionJoinner]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ChatSessionJoinner];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ChatContents'
CREATE TABLE [dbo].[ChatContents] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SessionId] int  NOT NULL,
    [SendBy] int  NOT NULL,
    [SendTo] int  NOT NULL,
    [MessageContent] varchar(max)  NOT NULL,
    [SendOn] datetime  NOT NULL
);
GO

-- Creating table 'ChatSessions'
CREATE TABLE [dbo].[ChatSessions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Topic] varchar(255)  NOT NULL,
    [StartBy] int  NOT NULL,
    [StartOn] datetime  NOT NULL,
    [IsFinished] bit  NOT NULL,
    [FinishedOn] datetime  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(50)  NOT NULL,
    [UserPassword] nvarchar(max)  NOT NULL,
    [UserFirstName] nvarchar(35)  NOT NULL,
    [UserLastName] nvarchar(35)  NOT NULL,
    [UserGender] nvarchar(1)  NOT NULL,
    [UserIsActive] bit  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [LastLogOnDate] datetime  NOT NULL,
    [UserType] int  NOT NULL,
    [UserIsOnLine] bit  NOT NULL
);
GO

-- Creating table 'ChatSessionJoinners'
CREATE TABLE [dbo].[ChatSessionJoinners] (
    [SessionId] int  NOT NULL,
    [UserId] int  NOT NULL,
    [JoinedOn] datetime  NULL,
    [LeftOn] datetime  NULL,
    [ConnectionID] uniqueidentifier  NULL,
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ChatContents'
ALTER TABLE [dbo].[ChatContents]
ADD CONSTRAINT [PK_ChatContents]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ChatSessions'
ALTER TABLE [dbo].[ChatSessions]
ADD CONSTRAINT [PK_ChatSessions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [Id] in table 'ChatSessionJoinners'
ALTER TABLE [dbo].[ChatSessionJoinners]
ADD CONSTRAINT [PK_ChatSessionJoinners]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [SessionId] in table 'ChatContents'
ALTER TABLE [dbo].[ChatContents]
ADD CONSTRAINT [FK__ChatConte__Sessi__4277DAAA]
    FOREIGN KEY ([SessionId])
    REFERENCES [dbo].[ChatSessions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK__ChatConte__Sessi__4277DAAA'
CREATE INDEX [IX_FK__ChatConte__Sessi__4277DAAA]
ON [dbo].[ChatContents]
    ([SessionId]);
GO

-- Creating foreign key on [StartBy] in table 'ChatSessions'
ALTER TABLE [dbo].[ChatSessions]
ADD CONSTRAINT [FK__ChatSessi__Start__3F9B6DFF]
    FOREIGN KEY ([StartBy])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK__ChatSessi__Start__3F9B6DFF'
CREATE INDEX [IX_FK__ChatSessi__Start__3F9B6DFF]
ON [dbo].[ChatSessions]
    ([StartBy]);
GO

-- Creating foreign key on [SessionId] in table 'ChatSessionJoinners'
ALTER TABLE [dbo].[ChatSessionJoinners]
ADD CONSTRAINT [FK__ChatSessi__Sessi__4A18FC72]
    FOREIGN KEY ([SessionId])
    REFERENCES [dbo].[ChatSessions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK__ChatSessi__Sessi__4A18FC72'
CREATE INDEX [IX_FK__ChatSessi__Sessi__4A18FC72]
ON [dbo].[ChatSessionJoinners]
    ([SessionId]);
GO

-- Creating foreign key on [UserId] in table 'ChatSessionJoinners'
ALTER TABLE [dbo].[ChatSessionJoinners]
ADD CONSTRAINT [FK__ChatSessi__UserI__4B0D20AB]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK__ChatSessi__UserI__4B0D20AB'
CREATE INDEX [IX_FK__ChatSessi__UserI__4B0D20AB]
ON [dbo].[ChatSessionJoinners]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------