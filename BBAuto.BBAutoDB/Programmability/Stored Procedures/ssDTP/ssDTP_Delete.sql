CREATE PROCEDURE [dbo].[ssDTP_Delete]
@idMark int
AS
BEGIN
	DELETE FROM ssDTP WHERE mark_id=@idMark
END
GO
