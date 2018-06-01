CREATE PROCEDURE [dbo].[Route_Delete]
@id int
AS
BEGIN
	DELETE FROM Route WHERE route_id=@id
END
GO
