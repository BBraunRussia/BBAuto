CREATE PROCEDURE [dbo].[MyPoint_Insert]
@id int,
@idRegion int,
@name varchar(100)
AS
BEGIN
	if (@id = 0)
	begin
		Declare @count int
		SELECT @count=count(*) FROM MyPoint WHERE mypoint_name=@name and region_id=@idRegion
		
		if (@count = 0)
		begin
			INSERT INTO MyPoint VALUES(@idRegion, @name)
			SET @id = SCOPE_IDENTITY()
		end
	end
	else
	begin
		UPDATE MyPoint SET region_id=@idRegion, mypoint_name=@name WHERE mypoint_id=@id
	end
	
	SELECT @id
END
GO
