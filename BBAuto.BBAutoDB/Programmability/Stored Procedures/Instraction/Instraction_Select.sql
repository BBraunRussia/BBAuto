CREATE PROCEDURE [dbo].[Instraction_Select]
AS
BEGIN
	SELECT instraction_id, instraction_number, instraction_date, driver_id, instraction_file
	FROM Instraction
END
GO
