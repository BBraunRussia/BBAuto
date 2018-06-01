CREATE PROCEDURE [dbo].[Route_Insert]
@id int,
@idMyPoint1 int,
@idMyPoint2 int,
@distance int
AS
BEGIN	
	if (@id = 0)
	begin
		Declare @count int
		SELECT @count=COUNT(*) FROM Route WHERE (mypoint1_id=@idMyPoint1 and mypoint2_id=@idMyPoint2) or (mypoint1_id=@idMyPoint2 and mypoint2_id=@idMyPoint1)
	
		if (@count = 0)
		begin
			INSERT INTO Route VALUES(@idMyPoint1, @idMyPoint2, @distance)
			SET @id=SCOPE_IDENTITY()
		end
	end
	else
	begin
		UPDATE Route SET route_distance=@distance WHERE route_id=@id
	end
	
	SELECT @id
END
GO
