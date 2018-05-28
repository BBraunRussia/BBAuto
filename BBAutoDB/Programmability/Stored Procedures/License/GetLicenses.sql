create procedure [dbo].[GetLicenses]
as
  select
    Id,
    DriverId,
    Number,
    DateBegin,
    DateEnd,
    [File],
    NotificationSent
  from
    License
