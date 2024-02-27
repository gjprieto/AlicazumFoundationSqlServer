IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'security')
	EXEC('CREATE SCHEMA [security] AUTHORIZATION [dbo]');