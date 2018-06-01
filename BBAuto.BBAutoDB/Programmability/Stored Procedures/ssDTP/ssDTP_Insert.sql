CREATE PROCEDURE [dbo].[ssDTP_Insert]
@idMark int,
@idServiceStantion int
AS
BEGIN
	Declare @count int
	SELECT @count=count(mark_id) FROM ssDTP WHERE mark_id=@idMark
	if (@count = 0)
		INSERT INTO ssDTP VALUES(@idMark, @idServiceStantion)
	else
		UPDATE ssDTP SET serviceStantion_id=@idServiceStantion
END
GO
