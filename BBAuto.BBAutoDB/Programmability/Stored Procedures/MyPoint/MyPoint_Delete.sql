CREATE PROCEDURE [dbo].[MyPoint_Delete]
@id int
AS
BEGIN
	Declare @count int	
	SELECT @count=COUNT(*) FROM Route WHERE mypoint1_id=@id or mypoint2_id=@id
	
	if (@count = 0)
		DELETE FROM MyPoint WHERE mypoint_id=@id
END
GO
