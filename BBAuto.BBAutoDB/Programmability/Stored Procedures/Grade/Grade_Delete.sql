CREATE PROCEDURE [dbo].[Grade_Delete]
@id int
AS
BEGIN
	DELETE FROM Grade WHERE grade_id=@id
	SELECT 'Удален'
END
GO
