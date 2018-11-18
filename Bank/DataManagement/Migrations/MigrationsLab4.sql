USE [bankdb];
GO

ALTER TABLE
		[dbo].[Accounts]
	ADD
		[PIN] VARCHAR(4) DEFAULT NULL;
GO

UPDATE 
		[dbo].[Accounts]
	SET
		[PIN] = '1111'
	WHERE
		[AccountNumber] NOT LIKE '7327%' 
		AND
		[AccountNumber] NOT LIKE '0101%';
GO