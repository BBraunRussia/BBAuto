CREATE PROCEDURE [dbo].[RepairType_Delete]
@idRepairType int
AS
BEGIN
	DELETE FROM RepairType WHERE RepairType_id=@idRepairType
END
GO
