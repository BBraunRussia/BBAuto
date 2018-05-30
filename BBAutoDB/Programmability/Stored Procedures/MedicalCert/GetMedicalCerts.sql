create procedure [dbo].[GetMedicalCerts]
as
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
