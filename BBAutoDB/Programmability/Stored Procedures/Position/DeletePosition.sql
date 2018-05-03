create procedure [dbo].[DeletePosition]
  @id int
as
begin
  delete from Position
  where position_id = @id
end
