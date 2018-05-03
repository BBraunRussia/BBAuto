create procedure [dbo].[GetRepairTypes]
  @actual int = 0
as
begin
  select repairType_id, repairType_name 'Название' from RepairType
end
