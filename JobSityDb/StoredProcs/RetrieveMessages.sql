CREATE PROCEDURE [dbo].[RetrieveMessages]
	
AS
	SELECT top(50) chat.UserId, u.UserName, chat.Message, chat.CreateDate FROM [dbo].chatMessage chat inner join dbo.Users u on u.UserId = chat.UserId order by chat.CreateDate desc

RETURN 0
