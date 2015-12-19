
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/19/2015 10:19:30
-- Generated from EDMX file: D:\informatica\ASP.net MVC\ProjectWerk\34 cursus - kopie\de Heerbeelding\Kunst\Models\CursusDataSet.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [EventsConnection];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CursusInschrijving]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CursusInschrijving] DROP CONSTRAINT [FK_CursusInschrijving];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Cursus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cursus];
GO
IF OBJECT_ID(N'[dbo].[CursusInschrijving]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CursusInschrijving];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Cursus'
CREATE TABLE [dbo].[Cursus] (
    [CursusId] int IDENTITY(1,1) NOT NULL,
    [CursusNaam] nvarchar(20)  NOT NULL,
    [CursusInfo] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'CursusInschrijving'
CREATE TABLE [dbo].[CursusInschrijving] (
    [CursusInschrijvingId] int IDENTITY(1,1) NOT NULL,
    [VoorNaam] nvarchar(20)  NOT NULL,
    [AchterNaam] nvarchar(20)  NOT NULL,
    [Email] nvarchar(50)  NULL,
    [TelefoonGsm] nvarchar(20)  NULL,
    [Cursus] int  NOT NULL,
    [Datum] nvarchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CursusId] in table 'Cursus'
ALTER TABLE [dbo].[Cursus]
ADD CONSTRAINT [PK_Cursus]
    PRIMARY KEY CLUSTERED ([CursusId] ASC);
GO

-- Creating primary key on [CursusInschrijvingId] in table 'CursusInschrijving'
ALTER TABLE [dbo].[CursusInschrijving]
ADD CONSTRAINT [PK_CursusInschrijving]
    PRIMARY KEY CLUSTERED ([CursusInschrijvingId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Cursus] in table 'CursusInschrijving'
ALTER TABLE [dbo].[CursusInschrijving]
ADD CONSTRAINT [FK_CursusInschrijving]
    FOREIGN KEY ([Cursus])
    REFERENCES [dbo].[Cursus]
        ([CursusId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CursusInschrijving'
CREATE INDEX [IX_FK_CursusInschrijving]
ON [dbo].[CursusInschrijving]
    ([Cursus]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------