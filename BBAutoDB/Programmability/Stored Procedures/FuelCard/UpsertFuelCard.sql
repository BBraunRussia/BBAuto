create procedure [dbo].[UpsertFuelCard]
  @id int,
  @idFuelCardType int,
  @number nvarchar(50),
  @dateEndText nvarchar(50),
  @RegionId int,
  @pin nvarchar(4),
  @lost bit,
  @comment nvarchar(100)
as
begin
  declare @dateEnd datetime

  if (@dateEndText = '')
    set @dateEnd = null
  else
    set @dateEnd = cast(@dateEndText as datetime)

  if (@id = 0)
  begin
    declare @count int
    select
      @count = count(*)
    from
      FuelCard
    where
      Number = @number

    if (@count = 0)
    begin
      insert into FuelCard(FuelCardTypeId, Number, DateEnd, RegionId, Pin, Lost, Comment)
        values(@idFuelCardType, @number, @dateEnd, @RegionId, @pin, @lost, @comment)
      set @id = scope_identity()
    end
  end
  else
    update FuelCard
    set FuelCardTypeId = @idFuelCardType,
        Number = @number,
        DateEnd = @dateEnd,
        RegionId = @RegionId,
        Pin = @pin,
        Lost = @lost,
        Comment = @comment
    where Id = @id

  select @id
end
