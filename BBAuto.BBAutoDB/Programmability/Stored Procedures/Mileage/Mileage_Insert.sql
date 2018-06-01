CREATE PROCEDURE [dbo].[Mileage_Insert]
@idMileage int,
@idCar int,
@date datetime,
@count int
AS
BEGIN
	if (@idMileage = 0)
	begin
		INSERT INTO Mileage VALUES(@idCar, @date, @count)
		
		SET @idMileage = SCOPE_IDENTITY()
	end
	else
		UPDATE Mileage SET mileage_date=@date, mileage_count=@count WHERE mileage_id=@idMileage	
	
	SELECT @idMileage
END
GO
