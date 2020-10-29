CREATE TABLE [dbo].[Users]
(
	[UserId] INT IDENTITY(1,1)PRIMARY KEY, 
    [UserName] VARCHAR(50) NOT NULL, 
    [UserPwd] VARCHAR(10) NOT NULL, 
    [CreatedDate] DATETIME NULL
)
