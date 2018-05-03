create procedure [dbo].[DeleteDriverLicense]
  @idLicense int
as
begin
  delete from DriverLicense
  where DriverLicense_id = @idLicense
end
