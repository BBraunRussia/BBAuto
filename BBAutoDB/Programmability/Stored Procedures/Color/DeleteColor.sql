create procedure [dbo].[DeleteColor]
  @idColor int
as
begin
  delete from Color
  where Color_id = @idColor
end
