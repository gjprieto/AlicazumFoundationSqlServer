IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'custom')
	EXEC('CREATE SCHEMA [custom] AUTHORIZATION [dbo]');