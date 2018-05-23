create procedure [dbo].[DeleteMedicalCert]
  @id int
as
begin
  delete from MedicalCert
  where Id = @id
end
