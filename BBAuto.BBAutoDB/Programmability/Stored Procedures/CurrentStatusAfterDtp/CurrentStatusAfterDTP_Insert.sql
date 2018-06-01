CREATE PROCEDURE [dbo].[CurrentStatusAfterDTP_Insert]
@id int,
@name varchar(100)
AS
BEGIN
	if (@id = 0)
	begin
		INSERT INTO CurrentStatusAfterDTP VALUES(@name)
		
		SET @id = SCOPE_IDENTITY()
	end
	else
		UPDATE CurrentStatusAfterDTP SET CurrentStatusAfterDTP_name = @name
		WHERE CurrentStatusAfterDTP_id=@id
		
	SELECT @id
END
GO
