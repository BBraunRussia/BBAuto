create procedure [dbo].[GetDriverLicenses]
as
begin
  select
    DriverLicense_id,
    driver_id,
    DriverLicense_number,
    DriverLicense_dateBegin,
    DriverLicense_dateEnd,
    DriverLicense_file,
    DriverLicense_notificationSent
  from
    DriverLicense
end
