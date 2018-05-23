create procedure [dbo].[DeleteSsDTP]
  @MarkId int
as
begin
  delete from ssDTP
  where MarkId = @MarkId
end
