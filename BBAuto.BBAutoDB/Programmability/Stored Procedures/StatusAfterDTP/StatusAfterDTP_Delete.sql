CREATE PROCEDURE [dbo].[StatusAfterDTP_Delete]
@id int
AS
BEGIN
	DELETE FROM StatusAfterDTP WHERE StatusAfterDTP_id=@id
END
GO
