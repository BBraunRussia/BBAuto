create procedure [dbo].[DeleteMedicalCert]
  @idMedicalCert int
as
begin
  delete from MedicalCert
  where medicalCert_id = @idMedicalCert
end
