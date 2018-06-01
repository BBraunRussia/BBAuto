CREATE PROCEDURE [dbo].[History_Insert]
@Comment varchar(50),
@id int,
@Event varchar(50),
@file varchar(MAX)
AS
BEGIN
	if (@file = '')
		insert into History values(@Comment, @id, CURRENT_TIMESTAMP, @Event, null)
	else
		insert into History values(@Comment, @id, CURRENT_TIMESTAMP, @Event, @file)
END
GO
