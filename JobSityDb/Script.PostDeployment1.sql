/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
GO
USE [JobSityDb]


GO
USE [JobSityDb]
GO
ALTER USER [JobSityU] WITH DEFAULT_SCHEMA=[db_owner]
GO
USE [JobSityDb]
GO
EXEC sp_addrolemember N'db_datareader', N'JobSityU'
GO
USE [JobSityDb]
GO
EXEC sp_addrolemember N'db_datawriter', N'JobSityU'
GO
USE [JobSityDb]
GO
EXEC sp_addrolemember N'db_owner', N'JobSityU'
GO

