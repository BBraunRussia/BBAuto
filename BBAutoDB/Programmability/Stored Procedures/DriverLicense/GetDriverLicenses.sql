create procedure [dbo].[GetDriverLicenses]
as
begin
  select
    Id,
    DriverId,
    Number,
    DateBegin,
    DateEnd,
    [File],
    NotificationSent
  from
    DriverLicense
end
