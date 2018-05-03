create procedure [dbo].[DeleteRepairType]
  @idRepairType int
as
begin
  delete from RepairType
  where repairType_id = @idRepairType
end
