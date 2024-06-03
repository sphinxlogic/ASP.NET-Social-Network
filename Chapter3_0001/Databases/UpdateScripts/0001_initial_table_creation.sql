/*
Script created by SQL Compare version 6.1.0 from Red Gate Software Ltd at 1/8/2008 6:22:35 AM
Run this script on localhost\sqlexpress.Empty to make it the same as localhost\sqlexpress.Fisharoo
Please back up your database before running this script
*/
SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
IF EXISTS (SELECT * FROM tempdb..sysobjects WHERE id=OBJECT_ID('tempdb..#tmpErrors')) DROP TABLE #tmpErrors
GO
CREATE TABLE #tmpErrors (Error int)
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO
IF NOT EXISTS (SELECT * FROM master.dbo.syslogins WHERE loginname = N'fisharoo_dev')
CREATE LOGIN [fisharoo_dev] WITH PASSWORD = 'fisharoo_devtm-es@as'
GO
IF EXISTS (select name from sys.sysusers where name = 'fisharoo_dev')
begin
exec sp_dropuser 'fisharoo_dev'
end
GO
CREATE USER [fisharoo_dev] FOR LOGIN [fisharoo_dev] WITH DEFAULT_SCHEMA=[dbo]
GO
GRANT CONNECT TO [fisharoo_dev]
GRANT DELETE TO [fisharoo_dev]
GRANT EXECUTE TO [fisharoo_dev]
GRANT INSERT TO [fisharoo_dev]
GRANT SELECT TO [fisharoo_dev]
GRANT UPDATE TO [fisharoo_dev]
BEGIN TRANSACTION
GO
PRINT N'Creating [dbo].[Accounts]'
GO
CREATE TABLE [dbo].[Accounts]
(
[AccountID] [int] NOT NULL IDENTITY(1, 1),
[FirstName] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LastName] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Email] [varchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Phone] [varchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Address1] [varchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Address2] [varchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Address3] [varchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[City] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[State] [varchar] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Zip] [varchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Country] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Username] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Password] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[BirthDate] [smalldatetime] NULL,
[CreateDate] [smalldatetime] NULL,
[LastUpdateDate] [smalldatetime] NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_Accounts] on [dbo].[Accounts]'
GO
ALTER TABLE [dbo].[Accounts] ADD CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED  ([AccountID])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[AccountPermissions]'
GO
CREATE TABLE [dbo].[AccountPermissions]
(
[apid] [int] NOT NULL IDENTITY(1, 1),
[AccountID] [int] NOT NULL,
[PermissionID] [int] NOT NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_AccountPermissions] on [dbo].[AccountPermissions]'
GO
ALTER TABLE [dbo].[AccountPermissions] ADD CONSTRAINT [PK_AccountPermissions] PRIMARY KEY CLUSTERED  ([apid])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Permissions]'
GO
CREATE TABLE [dbo].[Permissions]
(
[PermissionID] [int] NOT NULL IDENTITY(1, 1),
[Name] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_Permissions] on [dbo].[Permissions]'
GO
ALTER TABLE [dbo].[Permissions] ADD CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED  ([PermissionID])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating [dbo].[Profiles]'
GO
CREATE TABLE [dbo].[Profiles]
(
[ProfileID] [int] NOT NULL IDENTITY(1, 1),
[AccountID] [int] NOT NULL,
[ProfileName] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreateDate] [smalldatetime] NULL,
[LastUpdateDate] [smalldatetime] NULL
)

GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[AccountPermissions]'
GO
ALTER TABLE [dbo].[AccountPermissions] ADD
CONSTRAINT [FK_AccountPermissions_Accounts] FOREIGN KEY ([AccountID]) REFERENCES [dbo].[Accounts] ([AccountID]),
CONSTRAINT [FK_AccountPermissions_Permissions] FOREIGN KEY ([PermissionID]) REFERENCES [dbo].[Permissions] ([PermissionID])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[Profiles]'
GO
ALTER TABLE [dbo].[Profiles] ADD
CONSTRAINT [FK_Profiles_Accounts] FOREIGN KEY ([AccountID]) REFERENCES [dbo].[Accounts] ([AccountID])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
GRANT CONNECT TO [fisharoo_dev]
GRANT DELETE TO [fisharoo_dev]
GRANT EXECUTE TO [fisharoo_dev]
GRANT INSERT TO [fisharoo_dev]
GRANT SELECT TO [fisharoo_dev]
GRANT UPDATE TO [fisharoo_dev]
IF EXISTS (SELECT * FROM #tmpErrors) ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT>0 BEGIN
PRINT 'The database update succeeded'
COMMIT TRANSACTION
END
ELSE PRINT 'The database update failed'
GO
DROP TABLE #tmpErrors
GO