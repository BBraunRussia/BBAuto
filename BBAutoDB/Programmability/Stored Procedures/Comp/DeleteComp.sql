create procedure [dbo].[DeleteComp]
  @id int
as
begin
  delete from Comp
  where Id = @id
end
