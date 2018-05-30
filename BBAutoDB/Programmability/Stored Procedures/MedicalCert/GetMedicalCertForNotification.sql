create procedure [dbo].[GetMedicalCertForNotification]
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
  where
    NotificationSent = 0
