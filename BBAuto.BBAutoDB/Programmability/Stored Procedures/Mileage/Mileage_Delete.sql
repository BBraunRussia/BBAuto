CREATE PROCEDURE [dbo].[Mileage_Delete]
@id int
AS
BEGIN
	DELETE FROM Mileage WHERE mileage_id=@id
END
GO
