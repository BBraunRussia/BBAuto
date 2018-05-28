create procedure [dbo].[GetLicenseById]
  @id int
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
    Id = @id

