CREATE PROCEDURE [dbo].[ViolationType_Delete]
@idViolationType int
AS
BEGIN
	DELETE FROM ViolationType WHERE ViolationType_id=@idViolationType
END
GO
