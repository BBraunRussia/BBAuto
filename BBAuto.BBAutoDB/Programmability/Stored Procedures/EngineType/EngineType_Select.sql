CREATE PROCEDURE [dbo].[EngineType_Select]
AS
BEGIN
	SELECT engineType_id, engineType_name 'Название', engineType_shortName FROM EngineType
END
GO
