CREATE PROCEDURE [dbo].[TempMove_Insert]
@idTempMove int,
@idCar int,
@idDriver int,
@dateBegin datetime,
@dateEnd datetime
AS
BEGIN
	if (@idTempMove = 0)
	begin
		INSERT INTO TempMove VALUES(@idCar, @idDriver, @dateBegin, @dateEnd)
		
		SET @idTempMove = SCOPE_IDENTITY()
	end
	else
	begin
		UPDATE TempMove SET driver_id=@idDriver, tempMove_dateBegin=@dateBegin,
			tempMove_dateEnd=@dateEnd
		WHERE tempMove_id=@idTempMove
	end
	
	SELECT @idTempMove
END
GO
