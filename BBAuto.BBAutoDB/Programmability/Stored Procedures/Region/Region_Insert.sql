CREATE PROCEDURE [dbo].[Region_Insert]
@id int,
@Name varchar(50)
AS
BEGIN
	if (@id = 0)
	begin
		Declare @count int
		SELECT @count=count(region_id) FROM Region WHERE region_name=@Name
	
		if (@count = 0)
		begin
			INSERT INTO Region VALUES(@Name)
			SET @id = SCOPE_IDENTITY()
		end
	end
	else
	begin
		UPDATE Region SET region_name=@Name WHERE region_id=@id
	end
	
	SELECT @id
END
GO
