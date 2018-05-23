create procedure [dbo].[GetRepairTypes]
  @actual int = 0
as
begin
  select Id, [Name] from RepairType
end
