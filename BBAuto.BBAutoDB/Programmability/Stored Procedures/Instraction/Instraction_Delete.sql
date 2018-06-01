CREATE PROCEDURE [dbo].[Instraction_Delete]
@id int
AS
BEGIN
	DELETE FROM Instraction WHERE instraction_id=@id
END
GO
