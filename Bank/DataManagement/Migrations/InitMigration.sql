USE master;
GO

CREATE DATABASE [bankdb];
GO

USE [bankdb];
GO

CREATE TABLE [Clients]
(
	[Id] INT NOT NULL,
	[MiddleName] NVARCHAR(MAX) NOT NULL,
	[FirstName] NVARCHAR(MAX) NOT NULL,
	[LastName] NVARCHAR(MAX) NOT NULL,
	[BirthDate] DATETIME NOT NULL,
	[Gender] INT NOT NULL,
	[PassportSeries] VARCHAR(100) NOT NULL,
	[PassportNumber] VARCHAR(100) NOT NULL,
	[WhoAssiged] NVARCHAR(MAX) NOT NULL,
	[IdentityNumber] VARCHAR(200) NOT NULL,
	[PlaceBirth] NVARCHAR(MAX) NOT NULL,
	[CurrentCityId] INT NOT NULL,
	[CurrentAddress] NVARCHAR(MAX) NOT NULL,
	[HomePhone] VARCHAR(200) NOT NULL,
	[MobilePhone] VARCHAR(200) NOT NULL,
	[Email] VARCHAR(MAX) NOT NULL,
	[WorkPlace] NVARCHAR(MAX) NOT NULL,
	[WorkPosition] NVARCHAR(MAX) NOT NULL,
	[RegistrationCityId] INT NOT NULL,
	[RegistrationAddress] NVARCHAR(MAX) NOT NULL,
	[MaritalStatusId] INT NOT NULL,
	[CitizenshipId] INT NOT NULL,
	[DisabilityId] INT NOT NULL,
	[IsRetired] BIT NOT NULL,
	[MonthRevenue] DECIMAL(18,9) NOT NULL,
	[IsLiableForMilitaryService] BIT NOT NULL,
	PRIMARY KEY([Id])
);
GO

CREATE TABLE [Cities]
(
	[Id] INT NOT NULL,
	[Name] NVARCHAR(MAX) NOT NULL,
	PRIMARY KEY([Id])
);
GO

CREATE TABLE [Citizenships]
(
	[Id] INT NOT NULL,
	[CountryName] NVARCHAR(MAX) NOT NULL,
	PRIMARY KEY([Id])
);
GO

CREATE TABLE [Disabilities]
(
	[Id] INT NOT NULL,
	[Name] NVARCHAR(MAX) NOT NULL,
	PRIMARY KEY([Id])
);
GO

CREATE TABLE [MaritalStatuses]
(
	[Id] INT NOT NULL,
	[Name] NVARCHAR(MAX) NOT NULL,
	PRIMARY KEY([Id])
);
GO

ALTER TABLE [dbo].[Clients]
	ADD CONSTRAINT FK_Clients_CurrentCityId FOREIGN KEY ([CurrentCityId])
		REFERENCES [dbo].[Cities]([Id]);
GO

ALTER TABLE [dbo].[Clients]
	ADD CONSTRAINT FK_Clients_RegistrationCityId FOREIGN KEY ([RegistrationCityId])
		REFERENCES [dbo].[Cities]([Id]);
GO

ALTER TABLE [dbo].[Clients]
	ADD CONSTRAINT FK_Clients_DisabilityId FOREIGN KEY ([DisabilityId])
		REFERENCES [dbo].[Disabilities]([Id]);
GO

ALTER TABLE [dbo].[Clients]
	ADD CONSTRAINT FK_Clients_CitizenshipId FOREIGN KEY ([CitizenshipId])
		REFERENCES [dbo].[Citizenships]([Id]);
GO

ALTER TABLE [dbo].[Clients]
	ADD CONSTRAINT FK_Clients_MaritalStatusId FOREIGN KEY ([MaritalStatusId])
		REFERENCES [dbo].[MaritalStatuses]([Id]);
GO