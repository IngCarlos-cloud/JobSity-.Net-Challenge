CREATE PROCEDURE [dbo].[InsertUser]
	@userName  VARCHAR(50) ,
	@userPwd  VARCHAR(10) 
AS
	INSERT INTO dbo.Users (UserName,UserPwd,CreatedDate) values (@userName,@userPwd,GETDATE())

	select SCOPE_IDENTITY();
RETURN 0
