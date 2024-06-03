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
ALTER TABLE dbo.Accounts ADD
	TermsID int NOT NULL CONSTRAINT DF_Accounts_TermsID DEFAULT 0,
	AgreedToTermsDate smalldatetime NULL
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
CREATE TABLE dbo.Terms
	(
	TermID int NOT NULL IDENTITY (1, 1),
	Terms varchar(MAX) NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Terms', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Terms', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Terms', 'Object', 'CONTROL') as Contr_Per 

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
ALTER TABLE dbo.Terms ADD CONSTRAINT
	PK_Terms PRIMARY KEY CLUSTERED 
	(
	TermID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT

BEGIN TRANSACTION
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT

BEGIN TRANSACTION
GO
EXECUTE sp_rename N'dbo.Accounts.TermsID', N'Tmp_TermID_4', 'COLUMN' 
GO
EXECUTE sp_rename N'dbo.Accounts.Tmp_TermID_4', N'TermID', 'COLUMN' 
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Accounts', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Accounts', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Accounts', 'Object', 'CONTROL') as Contr_Per 

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
ALTER TABLE dbo.Terms ADD
	CreateDate smalldatetime NOT NULL CONSTRAINT DF_Terms_CreateDate DEFAULT getdate()
GO
COMMIT

select Has_Perms_By_Name(N'dbo.Terms', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Terms', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Terms', 'Object', 'CONTROL') as Contr_Per 


--create a dummy terms entry
insert into terms (terms)
values ('Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Vestibulum id tellus vel risus venenatis mattis. Donec ornare. Proin semper tortor. Donec ac dui ut odio mattis rhoncus. Sed at nibh vel diam accumsan pulvinar. Nam vel tortor eget tortor adipiscing mattis. Quisque ipsum. Aliquam odio. Pellentesque nisl justo, viverra sollicitudin, malesuada eget, lacinia sed, sapien. Sed accumsan dui rutrum massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Duis laoreet, magna ut ultrices ultrices, odio nisl viverra dolor, quis iaculis est sem id ligula. Integer aliquet, augue eu adipiscing porttitor, turpis lectus nonummy augue, sed tincidunt leo nisl sed lectus. Quisque consectetuer. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos hymenaeos.

Morbi vulputate tincidunt dolor. Aliquam ac nunc. Integer ac enim in urna varius tempor. Quisque pellentesque, ligula nec luctus egestas, nunc lacus consequat metus, vel tempus lacus nibh sit amet elit. Vivamus tincidunt viverra est. Proin mattis, enim et consectetuer laoreet, mi odio placerat pede, feugiat pretium massa nibh vel enim. Donec nonummy tincidunt tortor. Nunc dignissim nisl at risus. Ut lacinia. Proin egestas ultrices metus. Donec dignissim venenatis dui. Donec euismod. Phasellus sit amet velit sed elit laoreet pulvinar.

Pellentesque tellus. In eu leo. Nam tempor lacus vel elit. Pellentesque vel mauris. Integer venenatis enim id pede. Duis ipsum. Pellentesque eros metus, luctus non, feugiat eget, condimentum eget, tellus. Pellentesque eleifend, turpis eget congue accumsan, risus magna vehicula mauris, non lobortis risus diam tincidunt nisl. Aenean felis est, congue in, porttitor id, fermentum nec, orci. Pellentesque nonummy, neque ac dapibus ornare, mauris lacus scelerisque turpis, ac hendrerit leo quam et est. Sed nec augue vitae nunc tincidunt sollicitudin. Integer elementum. Sed dui urna, hendrerit sit amet, ultricies pharetra, suscipit non, felis. Sed odio. Praesent volutpat molestie pede. Sed varius aliquet nibh. Etiam molestie urna sed mi. Ut semper ante sed neque. Nam scelerisque consectetuer turpis.

Pellentesque pellentesque nunc sit amet nunc. Nulla nec sapien. Integer nec quam ut nulla feugiat scelerisque. Nulla facilisi. Donec eros ipsum, congue vel, viverra in, vehicula at, dolor. Nam pede pede, congue facilisis, rhoncus vitae, egestas sollicitudin, eros. Curabitur vitae nulla non quam feugiat nonummy. Cras condimentum aliquet leo. Pellentesque euismod volutpat urna. Maecenas porta. Quisque quis neque et diam condimentum faucibus. Maecenas vel nisl et purus convallis rhoncus. Suspendisse potenti. Donec faucibus congue ipsum.

Aliquam erat volutpat. Suspendisse sed felis. Donec quis risus a felis pulvinar tristique. Duis a sapien eu mauris scelerisque imperdiet. Maecenas vitae leo. Phasellus nisl. Maecenas leo. Donec ornare. Etiam adipiscing hendrerit mi. Suspendisse libero leo, porta ut, posuere at, facilisis ac, metus. Curabitur leo. Vivamus nec justo. Duis vel pede. Aliquam aliquam facilisis lorem. Cras eu elit quis turpis lacinia molestie. Duis a dolor eget ligula tempus condimentum.
')

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
ALTER TABLE dbo.Terms ADD
	Timestamp timestamp NOT NULL
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Terms', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Terms', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Terms', 'Object', 'CONTROL') as Contr_Per 

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
	DF_Accounts_AgreedToTermsDate DEFAULT getdate() FOR AgreedToTermsDate
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Accounts', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Accounts', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Accounts', 'Object', 'CONTROL') as Contr_Per 


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
ALTER TABLE dbo.Accounts
	DROP CONSTRAINT DF_Accounts_TermsID
GO
ALTER TABLE dbo.Accounts
	DROP CONSTRAINT DF_Accounts_AgreedToTermsDate
GO
CREATE TABLE dbo.Tmp_Accounts
	(
	AccountID int NOT NULL IDENTITY (1, 1),
	FirstName varchar(30) NULL,
	LastName varchar(30) NULL,
	Email varchar(150) NULL,
	EmailVerified bit NOT NULL,
	Zip varchar(15) NULL,
	Username varchar(30) NULL,
	Password varchar(50) NULL,
	BirthDate smalldatetime NULL,
	CreateDate smalldatetime NULL,
	LastUpdateDate smalldatetime NULL,
	Timestamp timestamp NOT NULL,
	TermID int NOT NULL,
	AgreedToTermsDate smalldatetime NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Accounts ADD CONSTRAINT
	DF_Accounts_EmailVerified DEFAULT 0 FOR EmailVerified
GO
ALTER TABLE dbo.Tmp_Accounts ADD CONSTRAINT
	DF_Accounts_CreateDate DEFAULT (getdate()) FOR CreateDate
GO
ALTER TABLE dbo.Tmp_Accounts ADD CONSTRAINT
	DF_Accounts_TermsID DEFAULT ((0)) FOR TermID
GO
ALTER TABLE dbo.Tmp_Accounts ADD CONSTRAINT
	DF_Accounts_AgreedToTermsDate DEFAULT (getdate()) FOR AgreedToTermsDate
GO
SET IDENTITY_INSERT dbo.Tmp_Accounts ON
GO
IF EXISTS(SELECT * FROM dbo.Accounts)
	 EXEC('INSERT INTO dbo.Tmp_Accounts (AccountID, FirstName, LastName, Email, Zip, Username, Password, BirthDate, CreateDate, LastUpdateDate, TermID, AgreedToTermsDate)
		SELECT AccountID, FirstName, LastName, Email, Zip, Username, Password, BirthDate, CreateDate, LastUpdateDate, TermID, AgreedToTermsDate FROM dbo.Accounts WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Accounts OFF
GO
ALTER TABLE dbo.AccountPermissions
	DROP CONSTRAINT FK_AccountPermissions_Accounts
GO
ALTER TABLE dbo.Profiles
	DROP CONSTRAINT FK_Profiles_Accounts
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
select Has_Perms_By_Name(N'dbo.Accounts', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Accounts', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Accounts', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
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

BEGIN TRANSACTION
select Has_Perms_By_Name(N'dbo.Profiles', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Profiles', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Profiles', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
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
select Has_Perms_By_Name(N'dbo.AccountPermissions', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.AccountPermissions', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.AccountPermissions', 'Object', 'CONTROL') as Contr_Per 