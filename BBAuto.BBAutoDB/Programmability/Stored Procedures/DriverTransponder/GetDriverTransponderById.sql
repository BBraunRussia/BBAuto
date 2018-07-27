create procedure [dbo].[GetDriverTransponderById]
  @id int
as
  select
    dt.Id,
    dt.TransponderId,
    dt.DriverId,
    dt.DateBegin,
    dt.DateEnd
  from
    DriverTransponder dt
  where
    dt.Id = @id
