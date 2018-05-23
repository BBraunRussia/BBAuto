create procedure [dbo].[UpsertFuelCardDriver]
  @id int,
  @FuelCardId int,
  @DriverId int,
  @dateBegin datetime,
  @dateEndText nvarchar(50)
as
begin
  declare @dateEnd datetime
  if (@dateEndText != '')
    set @dateEnd = cast(@dateEndText as datetime)
  else
    set @dateEnd = null

  if (@id = 0)
  begin
    insert into FuelCardDriver values(@FuelCardId, @DriverId, @dateBegin, @dateEnd)
    set @id = scope_identity()
  end
  else
    update FuelCardDriver
    set FuelCardId = @FuelCardId,
        DriverId = @DriverId,
        DateBegin = @dateBegin,
        DateEnd = @dateEnd
    where Id = @id

  select @id
end
