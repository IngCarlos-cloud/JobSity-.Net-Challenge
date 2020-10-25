CREATE PROCEDURE [dbo].[ValidateUser]
	@userName NVARCHAR(50),
	@password NVARCHAR(10)
AS
	SELECT UserId, UserName from [dbo].[Users] where UserName = @userName and UserPwd= @password
RETURN 0

