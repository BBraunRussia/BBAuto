create procedure [dbo].[DeleteDriverLicense]
  @id int
as
begin
  delete from DriverLicense
  where Id = @id
end
