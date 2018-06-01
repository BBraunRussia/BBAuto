CREATE PROCEDURE [dbo].[Color_Delete]
  @idColor int
AS
BEGIN
	DELETE FROM Color WHERE Color_id=@idColor
END
GO
