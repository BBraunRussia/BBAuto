CREATE PROCEDURE [dbo].[Region_Delete]
@id int
AS
BEGIN
	DELETE FROM Region WHERE region_id=@id
END
GO
