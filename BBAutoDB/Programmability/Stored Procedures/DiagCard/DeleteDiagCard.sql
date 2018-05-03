create procedure [dbo].[DeleteDiagCard]
  @idDiagCard int
as
begin
  delete from diagCard
  where diagCard_id = @idDiagCard
end
