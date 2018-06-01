CREATE PROCEDURE [dbo].[Mark_Select]
@all int = 0
AS
BEGIN
	SELECT mark_id, mark_name 'Название' FROM Mark ORDER BY mark_name
END
GO
