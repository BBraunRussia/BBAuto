CREATE PROCEDURE [dbo].[CurrentStatusAfterDTP_Delete]
@id int
AS
BEGIN
	DELETE FROM CurrentStatusAfterDTP WHERE CurrentStatusAfterDTP_id=@id
END
GO
