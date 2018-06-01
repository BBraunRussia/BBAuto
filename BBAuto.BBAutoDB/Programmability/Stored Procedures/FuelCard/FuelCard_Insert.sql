CREATE PROCEDURE [dbo].[FuelCard_Insert]
@idFuelCard int,
@idFuelCardType int,
@number varchar(50),
@dateEndText varchar(50),
@idRegion int,
@pin varchar(4),
@lost int,
@comment varchar(100)
AS
BEGIN
	Declare @dateEnd datetime
	
	if (@dateEndText = '')
		SET @dateEnd = null
	else
		SET @dateEnd = CAST(@dateEndText as datetime)
	
	if (@idFuelCard = 0)
	begin
		Declare @count int
		SELECT @count=COUNT(*) FROM FuelCard WHERE fuelCard_number=@number
		
		if (@count = 0)
		begin
			INSERT INTO FuelCard VALUES(@idFuelCardType, @number, @dateEnd, @idRegion,
				@pin, @lost, @comment)
			SET @idFuelCard = SCOPE_IDENTITY()
		end
	end
	else
		UPDATE FuelCard SET FuelCardType_id=@idFuelCardType, FuelCard_number=@number,
			FuelCard_dateEnd=@dateEnd, region_id=@idRegion, FuelCard_pin=@pin,
			FuelCard_lost=@lost, FuelCard_comment=@comment
		WHERE FuelCard_id = @idFuelCard
	
	SELECT @idFuelCard
END
GO
