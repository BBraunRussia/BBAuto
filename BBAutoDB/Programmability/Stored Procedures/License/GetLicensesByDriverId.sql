create procedure [dbo].[GetLicensesByDriverId]
  @driverId int
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
  where
    DriverId = @driverId
  order by
    DateBegin desc
