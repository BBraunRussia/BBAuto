CREATE PROCEDURE [dbo].[WayBillRoute_Insert]
@id int,
@idWayBillDay int,
@idMyPoint1 int,
@idMyPoint2 int,
@distance int
AS
BEGIN
	if (@id = 0)
	begin
		INSERT INTO WayBillRoute VALUES(@idWayBillDay,@idMyPoint1, @idMyPoint2, @distance)
		SET @id = SCOPE_IDENTITY()
	end
	
	SELECT @id
END
GO
