CREATE PROCEDURE [dbo].[MedicalCert_Delete]
@idMedicalCert int
AS
BEGIN
	DELETE FROM MedicalCert WHERE medicalCert_id=@idMedicalCert
END
GO
