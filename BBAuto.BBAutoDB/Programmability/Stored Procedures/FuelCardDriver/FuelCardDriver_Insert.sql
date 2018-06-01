CREATE PROCEDURE [dbo].[FuelCardDriver_Insert]
@idFuelCardDriver int,
@idFuelCard int,
@idDriver int,
@dateBegin datetime,
@dateEndText varchar(50)
AS
BEGIN
	Declare @dateEnd datetime
	if (@dateEndText != '')
		SET @dateEnd = CAST(@dateEndText as datetime)
	else
		SET @dateEnd = null
	
	if (@idFuelCardDriver = 0)
	begin
		INSERT INTO FuelCardDriver VALUES(@idFuelCard, @idDriver, @dateBegin, @dateEnd)
		SET @idFuelCardDriver = SCOPE_IDENTITY()
	end
	else
		UPDATE FuelCardDriver SET
			FuelCard_id = @idFuelCard, driver_id = @idDriver,
			FuelCardDriver_dateBegin = @dateBegin, FuelCardDriver_dateEnd = @dateEnd
		WHERE FuelCardDriver_id = @idFuelCardDriver
		
	SELECT @idFuelCardDriver
END
GO
