create procedure [dbo].[UpsertInvoice]
  @idInvoice int,
  @idCar int,
  @number int,
  @idDriverFrom int,
  @idDriverTo int,
  @date datetime,
  @dateMoveText nvarchar(50),
  @idRegionFrom int,
  @idRegionTo int,
  @file nvarchar(500)
as
  declare @dateMove datetime

  if (@dateMoveText = '')
    set @dateMove = null
  else
    set @dateMove = cast(@dateMoveText as datetime)

  if (@idInvoice = 0)
  begin
    select
      @number = max(i.number) + 1
    from
      Invoice i
    where
      year(i.date) = year(@date)

    if (@number is null)
      set @number = 1

    insert into Invoice values(@idCar, @number, @idDriverFrom, @idDriverTo, @date, @dateMove, @idRegionFrom, @idRegionTo, @file)
    set @idInvoice = @@identity
  end
  else
  begin
    update Invoice
    set driverIdFrom = @idDriverFrom,
        driverIdTo = @idDriverTo,
        [date] = @date,
        dateMove = @dateMove,
        regionIdFrom = @idRegionFrom,
        regionIdTo = @idRegionTo,
        [file] = @file
    where Id = @idInvoice
  end

  select @idInvoice
