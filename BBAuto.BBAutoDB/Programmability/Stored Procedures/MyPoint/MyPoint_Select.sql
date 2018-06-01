CREATE PROCEDURE [dbo].[MyPoint_Select]
AS
BEGIN
	SELECT mypoint_id, region_id, mypoint_name FROM MyPoint
END
GO
