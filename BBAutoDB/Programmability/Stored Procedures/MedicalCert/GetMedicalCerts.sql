create procedure [dbo].[GetMedicalCerts]
as
begin
  select
    Id,
    Number,
    DateBegin,
    DateEnd,
    DriverId,
    [File],
    NotificationSent
  from
    MedicalCert
end
