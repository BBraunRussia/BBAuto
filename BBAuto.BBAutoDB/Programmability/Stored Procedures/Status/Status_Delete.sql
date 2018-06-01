CREATE PROCEDURE [dbo].[Status_Delete]
@id int
AS
BEGIN
	DELETE FROM Status WHERE Status_id=@id
END
GO
