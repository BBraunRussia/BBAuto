create procedure [dbo].[DeleteComp]
  @id int
as
begin
  delete from Comp
  where Comp_id = @id
end
