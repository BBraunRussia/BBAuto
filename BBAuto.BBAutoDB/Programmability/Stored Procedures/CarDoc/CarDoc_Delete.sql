CREATE PROCEDURE [dbo].[CarDoc_Delete]
@idCarDoc int
AS
BEGIN
	DELETE FROM CarDoc WHERE carDoc_id=@idCarDoc
END
GO
