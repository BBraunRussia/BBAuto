CREATE PROCEDURE [dbo].[UserAccess_Select]
AS
BEGIN
	SELECT driver_id, role_id FROM UserAccess
END
GO
