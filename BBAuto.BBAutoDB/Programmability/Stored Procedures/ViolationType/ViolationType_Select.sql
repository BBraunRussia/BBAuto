CREATE PROCEDURE [dbo].[ViolationType_Select]
@actual int = 0
AS
BEGIN
	SELECT ViolationType_id, ViolationType_name 'Название'
	FROM ViolationType
	ORDER BY ViolationType_name
END
GO
