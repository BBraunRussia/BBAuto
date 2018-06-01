CREATE PROCEDURE [dbo].[CarSale_Delete]
@id int
AS
BEGIN
	DELETE FROM CarSale WHERE car_id=@id
END
GO
