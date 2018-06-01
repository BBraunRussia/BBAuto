CREATE PROCEDURE [dbo].[SuppyAddress_Insert]
@idMyPoint int
AS
BEGIN
	Declare @idRegion int
	SELECT region_id FROM MyPoint WHERE myPoint_id=@idMyPoint
	
	Declare @count int
	
	SELECT @count=COUNT(*)
	FROM SuppyAddress sa
		join MyPoint mp on sa.myPoint_id=mp.myPoint_id
	WHERE sa.myPoint_id=@idMyPoint or mp.region_id=@idRegion
	
	if (@count = 0)
		INSERT INTO SuppyAddress VALUES(@idMyPoint)
END
GO
