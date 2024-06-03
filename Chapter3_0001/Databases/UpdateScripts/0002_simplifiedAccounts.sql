/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Accounts ADD CONSTRAINT
	DF_Accounts_CreateDate DEFAULT getdate() FOR CreateDate
GO
ALTER TABLE dbo.Accounts
	DROP COLUMN Phone, Address1, Address2, Address3, City, State, Country
GO
COMMIT
