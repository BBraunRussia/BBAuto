CREATE PROCEDURE [dbo].[RepairType_Select]
@actual int = 0
AS
BEGIN
	SELECT repairType_id, repairType_name 'Название' FROM RepairType
END
GO
