CREATE PROCEDURE [dbo].[EngineType_Delete]
@id int
AS
BEGIN
	DELETE FROM EngineType WHERE EngineType_id=@id
END
GO
