CREATE PROCEDURE [dbo].[CarDoc_Insert]
@idCarDoc int,
@idCar int,
@name varchar(50),
@file varchar(200)
AS
BEGIN
	BEGIN TRANSACTION
	if (@idCarDoc = 0)
	begin
		declare @count int 
		select @count = count(carDoc_id) from CarDoc
		where carDoc_name=@name and carDoc_file=@file and car_id = @idCar

		if(@count = 0 and @idCar != 0)
		begin
			INSERT INTO CarDoc VALUES(@idCar, @name, @file)
		
			SELECT @idCarDoc = @@IDENTITY

			IF @@ERROR <> 0
				ROLLBACK TRANSACTION
		end
	end
	else
	begin
		UPDATE CarDoc
		SET carDoc_name=@name, carDoc_file=@file
		WHERE carDoc_id=@idCarDoc

		IF @@ERROR <> 0
			ROLLBACK TRANSACTION
	end
	COMMIT TRANSACTION;

	SELECT @idCarDoc
END
GO
