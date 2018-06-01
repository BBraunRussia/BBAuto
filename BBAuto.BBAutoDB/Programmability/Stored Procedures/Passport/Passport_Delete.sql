CREATE PROCEDURE [dbo].[Passport_Delete]
@idPassport int
AS
BEGIN
	DELETE FROM Passport WHERE passport_id=@idPassport
END
GO
