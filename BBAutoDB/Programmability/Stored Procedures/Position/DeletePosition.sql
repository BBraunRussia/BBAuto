create procedure [dbo].[DeletePosition]
  @id int
as
begin
  delete from Position
  where Id = @id
end
