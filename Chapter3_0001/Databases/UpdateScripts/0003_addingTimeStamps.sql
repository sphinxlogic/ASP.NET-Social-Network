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
ALTER TABLE dbo.Accounts
	DROP CONSTRAINT DF_Accounts_CreateDate
GO
CREATE TABLE dbo.Tmp_Accounts
	(
	AccountID int NOT NULL IDENTITY (1, 1),
	FirstName varchar(30) NULL,
	LastName varchar(30) NULL,
	Email varchar(150) NULL,
	Zip varchar(15) NULL,
	Username varchar(30) NULL,
	Password varchar(50) NULL,
	BirthDate smalldatetime NULL,
	CreateDate smalldatetime NULL,
	LastUpdateDate smalldatetime NULL,
	Timestamp timestamp NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Accounts ADD CONSTRAINT
	DF_Accounts_CreateDate DEFAULT (getdate()) FOR CreateDate
GO
SET IDENTITY_INSERT dbo.Tmp_Accounts ON
GO
IF EXISTS(SELECT * FROM dbo.Accounts)
	 EXEC('INSERT INTO dbo.Tmp_Accounts (AccountID, FirstName, LastName, Email, Zip, Username, Password, BirthDate, CreateDate, LastUpdateDate)
		SELECT AccountID, FirstName, LastName, Email, Zip, Username, Password, BirthDate, CreateDate, LastUpdateDate FROM dbo.Accounts WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Accounts OFF
GO
ALTER TABLE dbo.Profiles
	DROP CONSTRAINT FK_Profiles_Accounts
GO
ALTER TABLE dbo.AccountPermissions
	DROP CONSTRAINT FK_AccountPermissions_Accounts
GO
DROP TABLE dbo.Accounts
GO
EXECUTE sp_rename N'dbo.Tmp_Accounts', N'Accounts', 'OBJECT' 
GO
ALTER TABLE dbo.Accounts ADD CONSTRAINT
	PK_Accounts PRIMARY KEY CLUSTERED 
	(
	AccountID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AccountPermissions ADD CONSTRAINT
	FK_AccountPermissions_Accounts FOREIGN KEY
	(
	AccountID
	) REFERENCES dbo.Accounts
	(
	AccountID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Profiles ADD CONSTRAINT
	FK_Profiles_Accounts FOREIGN KEY
	(
	AccountID
	) REFERENCES dbo.Accounts
	(
	AccountID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT


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
CREATE TABLE dbo.Tmp_Permissions
	(
	PermissionID int NOT NULL IDENTITY (1, 1),
	Name varchar(50) NULL,
	Timestamp timestamp NOT NULL
	)  ON [PRIMARY]
GO
SET IDENTITY_INSERT dbo.Tmp_Permissions ON
GO
IF EXISTS(SELECT * FROM dbo.Permissions)
	 EXEC('INSERT INTO dbo.Tmp_Permissions (PermissionID, Name)
		SELECT PermissionID, Name FROM dbo.Permissions WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Permissions OFF
GO
ALTER TABLE dbo.AccountPermissions
	DROP CONSTRAINT FK_AccountPermissions_Permissions
GO
DROP TABLE dbo.Permissions
GO
EXECUTE sp_rename N'dbo.Tmp_Permissions', N'Permissions', 'OBJECT' 
GO
ALTER TABLE dbo.Permissions ADD CONSTRAINT
	PK_Permissions PRIMARY KEY CLUSTERED 
	(
	PermissionID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.AccountPermissions ADD CONSTRAINT
	FK_AccountPermissions_Permissions FOREIGN KEY
	(
	PermissionID
	) REFERENCES dbo.Permissions
	(
	PermissionID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT


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
ALTER TABLE dbo.AccountPermissions
	DROP CONSTRAINT FK_AccountPermissions_Permissions
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Permissions', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Permissions', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Permissions', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.AccountPermissions
	DROP CONSTRAINT FK_AccountPermissions_Accounts
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Accounts', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Accounts', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Accounts', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_AccountPermissions
	(
	apid int NOT NULL IDENTITY (1, 1),
	AccountID int NOT NULL,
	PermissionID int NOT NULL,
	Timestamp timestamp NOT NULL
	)  ON [PRIMARY]
GO
SET IDENTITY_INSERT dbo.Tmp_AccountPermissions ON
GO
IF EXISTS(SELECT * FROM dbo.AccountPermissions)
	 EXEC('INSERT INTO dbo.Tmp_AccountPermissions (apid, AccountID, PermissionID)
		SELECT apid, AccountID, PermissionID FROM dbo.AccountPermissions WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_AccountPermissions OFF
GO
DROP TABLE dbo.AccountPermissions
GO
EXECUTE sp_rename N'dbo.Tmp_AccountPermissions', N'AccountPermissions', 'OBJECT' 
GO
ALTER TABLE dbo.AccountPermissions ADD CONSTRAINT
	PK_AccountPermissions PRIMARY KEY CLUSTERED 
	(
	apid
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.AccountPermissions ADD CONSTRAINT
	FK_AccountPermissions_Accounts FOREIGN KEY
	(
	AccountID
	) REFERENCES dbo.Accounts
	(
	AccountID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.AccountPermissions ADD CONSTRAINT
	FK_AccountPermissions_Permissions FOREIGN KEY
	(
	PermissionID
	) REFERENCES dbo.Permissions
	(
	PermissionID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.AccountPermissions', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.AccountPermissions', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.AccountPermissions', 'Object', 'CONTROL') as Contr_Per 

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
ALTER TABLE dbo.Profiles
	DROP CONSTRAINT FK_Profiles_Accounts
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Accounts', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Accounts', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Accounts', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Profiles
	(
	ProfileID int NOT NULL IDENTITY (1, 1),
	AccountID int NOT NULL,
	ProfileName varchar(100) NULL,
	CreateDate smalldatetime NULL,
	LastUpdateDate smalldatetime NULL,
	Timestamp timestamp NOT NULL
	)  ON [PRIMARY]
GO
SET IDENTITY_INSERT dbo.Tmp_Profiles ON
GO
IF EXISTS(SELECT * FROM dbo.Profiles)
	 EXEC('INSERT INTO dbo.Tmp_Profiles (ProfileID, AccountID, ProfileName, CreateDate, LastUpdateDate)
		SELECT ProfileID, AccountID, ProfileName, CreateDate, LastUpdateDate FROM dbo.Profiles WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Profiles OFF
GO
DROP TABLE dbo.Profiles
GO
EXECUTE sp_rename N'dbo.Tmp_Profiles', N'Profiles', 'OBJECT' 
GO
ALTER TABLE dbo.Profiles ADD CONSTRAINT
	FK_Profiles_Accounts FOREIGN KEY
	(
	AccountID
	) REFERENCES dbo.Accounts
	(
	AccountID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Profiles', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Profiles', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Profiles', 'Object', 'CONTROL') as Contr_Per 