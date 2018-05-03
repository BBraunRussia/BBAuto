create procedure [dbo].[DeleteSsDTP]
  @idMark int
as
begin
  delete from ssDTP
  where mark_id = @idMark
end
