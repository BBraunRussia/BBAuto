CREATE PROCEDURE [dbo].[Fuel_Insert]
@idFuelCard int,
@date datetime,
@count float,
@idEngineType int
AS
BEGIN
	Declare @id int
	
	SELECT @id = fuel_id
	FROM Fuel
	WHERE fuelCard_id=@idFuelCard and fuel_date=@date and engineType_id=@idEngineType
	
	if (@id is null)
	begin
		INSERT INTO Fuel VALUES(@idFuelCard, @date, @count, @idEngineType)
		
		SET @id = SCOPE_IDENTITY()
	end
	else
	begin
		UPDATE Fuel SET fuel_count = fuel_count + @count WHERE fuel_id=@id
	end
	
	SELECT @id
END
GO
