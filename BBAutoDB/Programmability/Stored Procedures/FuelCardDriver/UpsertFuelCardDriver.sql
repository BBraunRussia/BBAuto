create procedure [dbo].[UpsertFuelCardDriver]
  @idFuelCardDriver int,
  @idFuelCard int,
  @idDriver int,
  @dateBegin datetime,
  @dateEndText nvarchar(50)
as
begin
  declare @dateEnd datetime
  if (@dateEndText != '')
    set @dateEnd = cast(@dateEndText as datetime)
  else
    set @dateEnd = null

  if (@idFuelCardDriver = 0)
  begin
    insert into FuelCardDriver values(@idFuelCard, @idDriver, @dateBegin, @dateEnd)
    set @idFuelCardDriver = scope_identity()
  end
  else
    update FuelCardDriver
    set FuelCard_id = @idFuelCard,
        driver_id = @idDriver,
        FuelCardDriver_dateBegin = @dateBegin,
        FuelCardDriver_dateEnd = @dateEnd
    where FuelCardDriver_id = @idFuelCardDriver

  select @idFuelCardDriver
end
