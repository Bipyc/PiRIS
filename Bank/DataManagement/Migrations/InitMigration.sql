USE master;
GO

CREATE DATABASE [bankdb];
GO

USE [bankdb];
GO

CREATE TABLE [Clients]
(
	[Id] INT IDENTITY(1,1),
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
	[HomePhone] VARCHAR(200) DEFAULT NULL,
	[MobilePhone] VARCHAR(200) DEFAULT NULL,
	[Email] VARCHAR(MAX) DEFAULT NULL,
	[WorkPlace] NVARCHAR(MAX) DEFAULT NULL,
	[WorkPosition] NVARCHAR(MAX) DEFAULT NULL,
	[RegistrationCityId] INT NOT NULL,
	[RegistrationAddress] NVARCHAR(MAX) NOT NULL,
	[MaritalStatusId] INT NOT NULL,
	[CitizenshipId] INT NOT NULL,
	[DisabilityId] INT NOT NULL,
	[IsRetired] BIT NOT NULL,
	[MonthRevenue] DECIMAL(18,9) DEFAULT NULL,
	[IsLiableForMilitaryService] BIT NOT NULL,
	PRIMARY KEY([Id]),
	UNIQUE([PassportNumber]),
	UNIQUE([IdentityNumber])
);
GO

CREATE TABLE [Cities]
(
	[Id] INT IDENTITY(1,1),
	[Name] NVARCHAR(MAX) NOT NULL,
	PRIMARY KEY([Id])
);
GO

CREATE TABLE [Citizenships]
(
	[Id] INT IDENTITY(1,1),
	[CountryName] NVARCHAR(MAX) NOT NULL,
	PRIMARY KEY([Id])
);
GO

CREATE TABLE [Disabilities]
(
	[Id] INT IDENTITY(1,1),
	[Name] NVARCHAR(MAX) NOT NULL,
	PRIMARY KEY([Id])
);
GO

CREATE TABLE [MaritalStatuses]
(
	[Id] INT IDENTITY(1,1),
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

INSERT INTO
		[dbo].[Cities]
	VALUES
		('�����'),
		('������'),
		('�����'),
		('�������'),
		('������')
GO

INSERT INTO
		[dbo].[Citizenships]
	VALUES
		('��'),
		('��'),
		('������')
GO

INSERT INTO
		[dbo].[Disabilities]
	VALUES
		('�������'),
		('1-� �������'),
		('2-� �������'),
		('3-� �������'),
		('���')
GO

INSERT INTO
		[dbo].[MaritalStatuses]
	VALUES
		('������'),
		('�����'),
		('����������� ����')
GO
