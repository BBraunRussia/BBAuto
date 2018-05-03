create procedure [dbo].[GetMedicalCerts]
as
begin
  select
    MedicalCert_id,
    MedicalCert_number,
    MedicalCert_dateBegin,
    MedicalCert_dateEnd,
    driver_id,
    MedicalCert_file,
    MedicalCert_notificationSent
  from
    MedicalCert
end
