CREATE PROCEDURE [dbo].[Owner_Delete]
@id int
AS
BEGIN
	DELETE FROM Owner WHERE Owner_id=@id
END
GO
