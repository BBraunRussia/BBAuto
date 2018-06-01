CREATE PROCEDURE [dbo].[Culprit_Delete]
@id int
AS
BEGIN
	DELETE FROM culprit WHERE culprit_id=@id
END
GO
