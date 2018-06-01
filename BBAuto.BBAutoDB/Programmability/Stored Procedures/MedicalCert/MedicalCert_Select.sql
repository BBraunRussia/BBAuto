CREATE PROCEDURE [dbo].[MedicalCert_Select]
AS
BEGIN
	SELECT medicalCert_id, MedicalCert_number, MedicalCert_dateBegin, MedicalCert_dateEnd,
		driver_id, MedicalCert_file, MedicalCert_notificationSent
	FROM MedicalCert
END
GO
