CREATE PROCEDURE [dbo].[Dept_Select]
@actual int = 0
AS
BEGIN
	SELECT dept_id, dept_name 'Название' FROM Dept ORDER BY dept_name
END
GO
