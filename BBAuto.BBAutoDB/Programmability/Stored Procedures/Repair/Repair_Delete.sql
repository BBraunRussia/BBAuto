CREATE PROCEDURE [dbo].[Repair_Delete]
@idRepair int
AS
BEGIN
	DELETE FROM Repair WHERE repair_id=@idRepair
END
GO
