CREATE PROCEDURE [dbo].[DiagCard_Delete]
@idDiagCard int
AS
BEGIN
	DELETE FROM DiagCard WHERE diagCard_id=@idDiagCard
END
GO
