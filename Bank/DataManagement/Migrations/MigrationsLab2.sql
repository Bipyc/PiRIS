USE [bankdb];
GO

CREATE TABLE [dbo].[Accounts]
(
	[Id] INT IDENTITY(1, 1),
	[AccountNumber] VARCHAR(13) NOT NULL,
	[CreationDate] DATETIME2(7) NOT NULL,
	[CurrencyTypeId] INT NOT NULL,
	[Debit] DECIMAL(21, 9) NOT NULL,
	[Credit] DECIMAL(21, 9) NOT NULL,
	[Saldo] DECIMAL(21, 9) NOT NULL,
	[IsClosed] BIT NOT NULL DEFAULT 0,
	[ContractId] INT DEFAULT NULL,
	[ClientId] INT DEFAULT NULL,
	[AccountName] NVARCHAR(100) NOT NULL,
	PRIMARY KEY([Id]),
	UNIQUE([AccountNumber])
);

CREATE TABLE [dbo].[Transactions]
(
	[Id] INT IDENTITY(1, 1),
	[DateTime] DATETIME2(7),
	[AccountFromId] INT NOT NULL,
	[AccountToId] INT NOT NULL,
	[Amount] DECIMAL(21, 9) NOT NULL,
	[TransactionTypeId] INT NOT NULL,
	PRIMARY KEY([Id]) 
);

CREATE TABLE [dbo].[TransactionTypes]
(
	[Id] INT IDENTITY(1, 1),
	[Direction] NVARCHAR(100) NOT NULL,
	[Description] NVARCHAR(MAX) DEFAULT NULL,
	PRIMARY KEY([Id])
);

CREATE TABLE [dbo].[CurrencyTypes]
(
	[Id] INT IDENTITY(1, 1),
	[Name] NVARCHAR(100) NOT NULL,
	PRIMARY KEY([Id])
);

CREATE TABLE [dbo].[Contracts]
(
	[Id] INT IDENTITY(1, 1),
	[DateOfSign] DATETIME2(7) NOT NULL,
	[DateOfEnd] DATETIME2(7) NOT NULL,
	[Amount] DECIMAL(21, 9) NOT NULL,
	[DepositId] INT NOT NULL,
	[CreditId] INT NOT NULL,
	[CurrencyTypeId] INT NOT NULL,
	[ClientId] INT NOT NULL,
	PRIMARY KEY([Id])
);

CREATE TABLE [dbo].[Deposits]
(
	[Id] INT IDENTITY(1, 1),
	[Type] INT NOT NULL,
	[Name] NVARCHAR(MAX) NOT NULL,
	[Description] NVARCHAR(MAX) DEFAULT NULL,
	[MinExpirationTerm] INT NOT NULL,
	[MaxExpirationTerm] INT NOT NULL,
	PRIMARY KEY([Id])
);

CREATE TABLE [dbo].[YearProcentDepositCurrencies]
(
	[Id] INT IDENTITY(1, 1),
	[DepositId] INT NOT NULL,
	[CurrencyTypeId] INT NOT NULL,
	[Value] DECIMAL(21, 9) NOT NULL,
	PRIMARY KEY(Id)
);

ALTER TABLE 
		[dbo].[Accounts]
	ADD CONSTRAINT 
		FK_Account_CurrencyTypeId_CurrencyType_Id 
	FOREIGN KEY
	(
		[CurrencyTypeId]
	)
	REFERENCES
		[dbo].[CurrencyTypes]([Id]);
GO

ALTER TABLE 
		[dbo].[Accounts]
	ADD CONSTRAINT 
		FK_Account_ClientId_Clients_Id
	FOREIGN KEY
	(
		[ClientId]
	)
	REFERENCES 
		[dbo].[Clients]([Id]);
GO

ALTER TABLE 
		[dbo].[Accounts]
	ADD CONSTRAINT
		FK_Account_ContractId_Contracts_Id
	FOREIGN KEY
	(
		[ContractId]
	)
	REFERENCES
		[dbo].[Contracts]([Id]);
GO

ALTER TABLE
		[dbo].[Transactions]
	ADD CONSTRAINT
		FK_Transactions_AccountFromId_Accounts_Id
	FOREIGN KEY
	(
		[AccountFromId]
	)
	REFERENCES
		[dbo].[Accounts]([Id]);
GO

ALTER TABLE
		[dbo].[Transactions]
	ADD CONSTRAINT
		FK_Transactions_AccountToId_Accounts_Id
	FOREIGN KEY
	(
		[AccountToId]
	)
	REFERENCES
		[dbo].[Accounts]([Id]);
GO	

ALTER TABLE
		[dbo].[Transactions]
	ADD CONSTRAINT
		FK_Transactions_TransactionTypeId_Accounts_Id
	FOREIGN KEY
	(
		[AccountToId]
	)
	REFERENCES 
		[dbo].[Accounts]([Id]);
GO

ALTER TABLE
		[dbo].[Contracts]
	ADD CONSTRAINT
		FK_Contracts_ClientId_Clients_Id
	FOREIGN KEY
	(
		[ClientId]
	)
	REFERENCES 
		[dbo].[Clients]([Id]);
GO

ALTER TABLE
		[dbo].[Contracts]
	ADD CONSTRAINT
		FK_Contracts_CurrencyTypeId_CurrencyTypes_Id
	FOREIGN KEY
	(
		[CurrencyTypeId]
	)
	REFERENCES 
		[dbo].[CurrencyTypes]([Id]);
GO

ALTER TABLE
		[dbo].[Contracts]
	ADD CONSTRAINT
		FK_Contracts_DepositId_Deposits_Id
	FOREIGN KEY
	(
		[DepositId]
	)
	REFERENCES 
		[dbo].[Deposits]([Id]);
GO

ALTER TABLE
		[dbo].[YearProcentDepositCurrencies]
	ADD CONSTRAINT
		FK_YearProcentDepositCurrencies_DepositId_Deposits_Id
	FOREIGN KEY
	(
		[DepositId]
	)
	REFERENCES 
		[dbo].[Deposits]([Id]);
GO

ALTER TABLE
		[dbo].[YearProcentDepositCurrencies]
	ADD CONSTRAINT
		FK_YearProcentDepositCurrencies_CurrencyTypeId_CurrencyTypes_Id
	FOREIGN KEY
	(
		[CurrencyTypeId]
	)
	REFERENCES 
		[dbo].[CurrencyTypes]([Id]);
GO

INSERT INTO
		[dbo].[TransactionTypes]
	VALUES
		('From debit to debit', NULL),
		('From debit to credit', NULL),
		('From credit to credit', NULL),
		('From credit to debit', NULL),
		('Add to debit', NULL),
		('Add to credit', NULL);
GO

INSERT INTO
		[dbo].[CurrencyTypes]
	VALUES
		('BYN'),
		('RUB'),
		('USD'),
		('EUR');
GO

INSERT INTO
		[dbo].[Deposits]
	VALUES
		(0, 'Срочный отзывный', NULL, 7, 1500),
		(1, 'Срочный безотзывный', NULL, 30, 3000);
GO

INSERT INTO
		[dbo].[YearProcentDepositCurrencies]
	VALUES
		(1, 1, 6.9),
		(1, 2, 3.5),
		(1, 3, 1.2),
		(1, 4, 0.5);
GO

INSERT INTO
		[dbo].[YearProcentDepositCurrencies]
	VALUES
		(2, 1, 8.0),
		(2, 2, 3.75),
		(2, 3, 1.5),
		(2, 4, 0.7);
GO

DELETE FROM [dbo].[Accounts];

INSERT INTO
		[dbo].[Accounts]
	VALUES
		(
			'7327000000011',
			GETDATE(),
			1,
			100000000.0,
			100000000.0,
			0.0,
			0,
			NULL,
			NULL,
			'Фонд развития банка(BYN)'
		),
		(
			'7327000000021',
			GETDATE(),
			1,
			100000000.0,
			100000000.0,
			0.0,
			0,
			NULL,
			NULL,
			'Фонд развития банка(RUB)'
		),
		(
			'7327000000031',
			GETDATE(),
			1,
			100000000.0,
			100000000.0,
			0.0,
			0,
			NULL,
			NULL,
			'Фонд развития банка(USD)'
		),
		(
			'7327000000041',
			GETDATE(),
			1,
			100000000.0,
			100000000.0,
			0.0,
			0,
			NULL,
			NULL,
			'Фонд развития банка(EUR)'
		),
		(
			'0101000000011',
			GETDATE(),
			1,
			0.0,
			0.0,
			0.0,
			0,
			NULL,
			NULL,
			'Касса банка (BYN)'
		),
		(
			'0101000000021',
			GETDATE(),
			2,
			0.0,
			0.0,
			0.0,
			0,
			NULL,
			NULL,
			'Касса банка (RUB)'
		),
		(
			'0101000000031',
			GETDATE(),
			3,
			0.0,
			0.0,
			0.0,
			0,
			NULL,
			NULL,
			'Касса банка (USD)'
		),
		(
			'0101000000041',
			GETDATE(),
			3,
			0.0,
			0.0,
			0.0,
			0,
			NULL,
			NULL,
			'Касса банка (EUR)'
		);

CREATE TABLE [dbo].[Configs]
(
	[Id] INT IDENTITY(1, 1),
	[Name] VARCHAR(MAX),
	[Value] VARCHAR(MAX),
	PRIMARY KEY([Id])
);

INSERT INTO
		[dbo].[Configs]
	VALUES
		('LastIdForCreditAccounts', '2400000000001'),
		('LastIdForCurrentAccounts', '3014000000001'),
		('LastIdForCreditDepositAccounts', '1337000000001'),
		('LastIdForDepositAccounts', '1336000000001');
GO

SELECT * FROM [dbo].[Accounts];
