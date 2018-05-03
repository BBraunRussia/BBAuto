create procedure [dbo].[UpsertFuelCard]
  @idFuelCard int,
  @idFuelCardType int,
  @number nvarchar(50),
  @dateEndText nvarchar(50),
  @idRegion int,
  @pin nvarchar(4),
  @lost int,
  @comment nvarchar(100)
as
begin
  declare @dateEnd datetime

  if (@dateEndText = '')
    set @dateEnd = null
  else
    set @dateEnd = cast(@dateEndText as datetime)

  if (@idFuelCard = 0)
  begin
    declare @count int
    select
      @count = count(*)
    from
      FuelCard
    where
      FuelCard_number = @number

    if (@count = 0)
    begin
      insert into FuelCard values(@idFuelCardType, @number, @dateEnd, @idRegion, @pin, @lost, @comment)
      set @idFuelCard = scope_identity()
    end
  end
  else
    update FuelCard
    set FuelCardType_id = @idFuelCardType,
        FuelCard_number = @number,
        FuelCard_dateEnd = @dateEnd,
        region_id = @idRegion,
        FuelCard_pin = @pin,
        FuelCard_lost = @lost,
        FuelCard_comment = @comment
    where FuelCard_id = @idFuelCard

  select @idFuelCard
end
