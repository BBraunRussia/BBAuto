create procedure [dbo].[DeleteRepair]
  @id int
as
begin
  delete from Repair
  where Id = @id
end
