CREATE PROCEDURE [dbo].[CarSale_Insert]
@idCar int,
@comm varchar(100) = '',
@date varchar(50) = ''
AS
BEGIN
	Declare @count int
	SELECT @count=COUNT(car_id) FROM CarSale WHERE car_id=@idCar
	
	if (@count = 0)
	begin
		INSERT INTO CarSale VALUES(@idCar, null, null)
	end
	else
	begin
		if (@date = '')
			SET @date = null
		else
			SET @date = CAST(@date as datetime)
		UPDATE CarSale
		SET carSale_date=@date, carSale_comm=@comm
		WHERE car_id=@idCar
	end
END

GO
