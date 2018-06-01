CREATE PROCEDURE [dbo].[Instraction_Insert]
@id int,
@idDriver int,
@number varchar(50),
@date datetime,
@file varchar(100)
AS
BEGIN
	if (@id = 0)
	begin
		INSERT INTO Instraction VALUES(@number, @date, @idDriver, @file)
		
		SET @id = SCOPE_IDENTITY()
	end
	else
		UPDATE Instraction
		SET instraction_number=@number, instraction_date=@date, instraction_file=@file
		WHERE instraction_id=@id
		
	SELECT @id
END
GO
