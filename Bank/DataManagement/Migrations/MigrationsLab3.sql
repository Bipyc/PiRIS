USE [bankdb];
GO

CREATE TABLE [dbo].[Credits]
(
	[Id] INT IDENTITY(1, 1),
	[Type] INT NOT NULL,
	[Name] NVARCHAR(MAX) NOT NULL,
	[Description] NVARCHAR(MAX) DEFAULT NULL,
	[MinExpirationTerm] INT NOT NULL,
	[MaxExpirationTerm] INT NOT NULL,
	[MinAmount] DECIMAL(21, 9),
	[MaxAmount] DECIMAL(21, 9),
	PRIMARY KEY([Id])
);

CREATE TABLE [dbo].[YearProcentCreditCurrencies]
(
	[Id] INT IDENTITY(1, 1),
	[CreditId] INT NOT NULL,
	[CurrencyTypeId] INT NOT NULL,
	[Value] DECIMAL(21, 9) NOT NULL,
	PRIMARY KEY(Id)
);

ALTER TABLE
		[dbo].[Contracts]
	ADD CONSTRAINT
		FK_Contracts_CreditId_Credits_Id
	FOREIGN KEY
	(
		[CreditId]
	)
	REFERENCES 
		[dbo].[Credits]([Id]);
GO

ALTER TABLE
		[dbo].[YearProcentCreditCurrencies]
	ADD CONSTRAINT
		FK_YearProcentCreditCurrencies_CreditId_Credits_Id
	FOREIGN KEY
	(
		[CreditId]
	)
	REFERENCES 
		[dbo].[Credits]([Id]);
GO

ALTER TABLE
		[dbo].[YearProcentCreditCurrencies]
	ADD CONSTRAINT
		FK_YearProcentCreditCurrencies_CurrencyTypeId_CurrencyTypes_Id
	FOREIGN KEY
	(
		[CurrencyTypeId]
	)
	REFERENCES 
		[dbo].[CurrencyTypes]([Id]);
GO

INSERT INTO
		[dbo].[Credits]
	VALUES
		(0, 'Аннуитентный', NULL, 30, 24 * 30),
		(1, 'Дифференцированный', NULL, 30, 24 * 30);
GO

INSERT INTO
		[dbo].[YearProcentCreditCurrencies]
	VALUES
		(1, 1, 11.5),
		(1, 2, 11.75),
		(1, 3, 12.0),
		(1, 4, 12.0);
GO

INSERT INTO
		[dbo].[YearProcentCreditCurrencies]
	VALUES
		(2, 1, 11.5),
		(2, 2, 11.75),
		(2, 3, 12.0),
		(2, 4, 12.0);
GO