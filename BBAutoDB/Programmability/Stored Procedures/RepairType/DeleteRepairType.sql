create procedure [dbo].[DeleteRepairType]
  @id int
as
begin
  delete from RepairType
  where Id = @id
end
