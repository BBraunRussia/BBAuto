create procedure [dbo].[UpsertDriverTransponder]
  @id int,
  @transponderId int,
  @driverId int,
  @dateBegin datetime,
  @dateEnd datetime
as
  if not exists (select 1 from DriverTransponder dt where dt.Id = @id)
    insert into DriverTransponder(TransponderId, DriverId, DateBegin, DateEnd) values(@transponderId, @driverId, @dateBegin, @dateEnd);
  else
    update
      DriverTransponder
    set
      DriverId = @driverId,
      DateBegin = @dateBegin,
      DateEnd = @dateEnd
    where
      Id = @id
