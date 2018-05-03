create procedure [dbo].[DeleteDTP]
  @idDTP int
as
begin
  delete from DTP
  where dtp_id = @idDTP
end
