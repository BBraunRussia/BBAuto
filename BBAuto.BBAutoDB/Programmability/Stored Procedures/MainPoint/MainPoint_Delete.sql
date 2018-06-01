CREATE PROCEDURE [dbo].[MainPoint_Delete]
@id int
AS
BEGIN
	DELETE FROM MainPoint WHERE point_id=@id
END
GO
