CREATE PROCEDURE [dbo].[Diller_Delete]
@id int
AS
BEGIN
	DELETE FROM Diller WHERE diller_id=@id
END
GO
