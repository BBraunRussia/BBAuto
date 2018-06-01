CREATE PROCEDURE [dbo].[WayBillDay_Insert]
@id int,
@idCar int,
@idDriver int,
@date datetime
AS
BEGIN
	if (@id = 0)
	begin
		INSERT INTO WayBillDay VALUES(@idCar, @idDriver, @date)
		SET @id = SCOPE_IDENTITY()
	end
	
	SELECT @id
END
GO
