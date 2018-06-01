CREATE PROCEDURE [dbo].[Color_Select]
AS
BEGIN
	SELECT color_id, color_name 'Название' FROM Color ORDER BY Color_name
END
GO
