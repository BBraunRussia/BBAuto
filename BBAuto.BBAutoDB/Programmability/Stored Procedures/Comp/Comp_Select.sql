CREATE PROCEDURE [dbo].[Comp_Select]
AS
BEGIN
	SELECT comp_id, comp_name 'Название' FROM Comp
	ORDER BY Comp_name
END
GO
