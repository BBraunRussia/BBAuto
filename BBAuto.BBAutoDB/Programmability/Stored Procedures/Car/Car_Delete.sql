CREATE PROCEDURE [dbo].[Car_Delete]
@idCar int
AS
BEGIN
	DELETE FROM Car WHERE car_id=@idCar
END
GO
