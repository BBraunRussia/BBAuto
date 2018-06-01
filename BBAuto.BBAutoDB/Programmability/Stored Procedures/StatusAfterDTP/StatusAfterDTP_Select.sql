CREATE PROCEDURE [dbo].[StatusAfterDTP_Select]
@actual int = 0
AS
BEGIN
	SELECT StatusAfterDTP_id, StatusAfterDTP_name 'Название' FROM StatusAfterDTP
	ORDER BY StatusAfterDTP_name
END
GO
