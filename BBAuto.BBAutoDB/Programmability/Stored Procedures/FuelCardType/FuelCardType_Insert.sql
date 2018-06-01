CREATE PROCEDURE [dbo].[FuelCardType_Insert]
@idFuelCardType int,
@name varchar(50)
AS
BEGIN
	if (@idFuelCardType = 0)
	begin
		INSERT INTO FuelCardType VALUES(@name)
		
		SET @idFuelCardType = SCOPE_IDENTITY()
	end
	else
		UPDATE FuelCardType SET FuelCardType_name=@name
		
	SELECT @idFuelCardType
END
GO
