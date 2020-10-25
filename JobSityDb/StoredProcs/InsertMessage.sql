CREATE PROCEDURE [dbo].[InsertMessage]
	@UserId INT , 
    @Message VARCHAR(250) 
AS

	INSERT INTO [dbo].chatMessage (UserId,Message,CreateDate) values (@UserId,@Message,GETDATE())	

RETURN 0
