create procedure [dbo].[DeleteOwner]
  @id int
as
begin
  delete from Owner
  where owner_id = @id
end
