CREATE PROCEDURE [dbo].[Position_Select]
@actual int = 0
AS
BEGIN
	SELECT Position_id, Position_name 'Название' FROM Position ORDER BY Position_name
END
GO
