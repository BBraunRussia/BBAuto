create procedure [dbo].[DeleteViolationType]
  @idViolationType int
as
begin
  delete from ViolationType
  where violationType_id = @idViolationType
end
