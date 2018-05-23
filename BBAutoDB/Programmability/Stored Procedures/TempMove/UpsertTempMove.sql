create procedure [dbo].[UpsertTempMove]
  @id int,
  @CarId int,
  @DriverId int,
  @dateBegin datetime,
  @dateEnd datetime
as
begin
  if (@id = 0)
  begin
    insert into TempMove values(@CarId, @DriverId, @dateBegin, @dateEnd)

    set @id = scope_identity()
  end
  else
  begin
    update TempMove
    set DriverId = @DriverId,
        DateBegin = @dateBegin,
        DateEnd = @dateEnd
    where Id = @id
  end

  select @id
end
