CREATE PROCEDURE [dbo].[Violation_Delete]
@idViolation int
AS
BEGIN
	declare @filePay varchar(MAX)
	
	SELECT @filePay = violation_filePay FROM Violation WHERE violation_id=@idViolation

	DELETE FROM Violation WHERE violation_id=@idViolation
	
	exec History_Insert 'Violation', @idViolation, 'Delete', @filePay
	
END
GO
