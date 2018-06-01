CREATE PROCEDURE [dbo].[Position_Delete]
@id int
AS
BEGIN
	DELETE FROM Position WHERE Position_id=@id
END
GO
