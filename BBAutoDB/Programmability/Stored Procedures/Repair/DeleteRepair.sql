create procedure [dbo].[DeleteRepair]
  @idRepair int
as
begin
  delete from Repair
  where repair_id = @idRepair
end
