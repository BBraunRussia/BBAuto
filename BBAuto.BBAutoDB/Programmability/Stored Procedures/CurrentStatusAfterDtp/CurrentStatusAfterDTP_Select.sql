CREATE PROCEDURE [dbo].[CurrentStatusAfterDTP_Select]
AS
BEGIN
	SELECT CurrentStatusAfterDTP_id, CurrentStatusAfterDTP_name 'Название' FROM CurrentStatusAfterDTP
END
GO
