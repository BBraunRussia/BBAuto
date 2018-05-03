create procedure [dbo].[DeleteCarDoc]
  @idCarDoc int
as
begin
  delete from carDoc
  where carDoc_id = @idCarDoc
end
