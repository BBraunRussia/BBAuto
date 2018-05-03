create procedure [dbo].[UpsertTempMove]
  @idTempMove int,
  @idCar int,
  @idDriver int,
  @dateBegin datetime,
  @dateEnd datetime
as
begin
  if (@idTempMove = 0)
  begin
    insert into TempMove values(@idCar, @idDriver, @dateBegin, @dateEnd)

    set @idTempMove = scope_identity()
  end
  else
  begin
    update TempMove
    set driver_id = @idDriver,
        tempMove_dateBegin = @dateBegin,
        tempMove_dateEnd = @dateEnd
    where tempMove_id = @idTempMove
  end

  select @idTempMove
end
