CREATE PROCEDURE [dbo].[CarDoc_Select]
AS
BEGIN
	SELECT carDoc_id, car_id, carDoc_name, carDoc_file
	FROM CarDoc	
END
GO
