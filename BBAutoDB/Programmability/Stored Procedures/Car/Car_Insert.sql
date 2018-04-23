CREATE PROCEDURE [dbo].[Car_Insert]
@idCar int,
@bbNumber int,
@grz nvarchar(50),
@vin nvarchar(17),
@year int,
@eNumber nvarchar(50),
@bodyNumber nvarchar(50),
@idGrade int,
@idColor int,
@isLising int,
@LisingDate datetime,
@InvertoryNumber nvarchar(50)
AS
BEGIN
	if (@idColor = 0)
		SET @idColor = 1
		
	if (@isLising = 0)
		SET @LisingDate = NULL
		
	if (@idCar = 0)
	begin			
		INSERT INTO Car VALUES (@bbNumber, @grz, @vin, @year, @eNumber, @bodyNumber, NULL, NULL,
			@idGrade, @idColor, @LisingDate, @InvertoryNumber)
		SET @idCar = SCOPE_IDENTITY()
	end
	else
	begin		
		UPDATE Car
		SET car_grz=@grz, car_vin=@vin, car_year=@year, car_eNumber=@eNumber,
			car_bodyNumber=@bodyNumber, grade_id=@idGrade, color_id=@idColor, car_lisingDate=@LisingDate,
			car_InvertoryNumber=@InvertoryNumber
		WHERE car_id=@idCar
	end
	
	SELECT @idCar
END
GO
