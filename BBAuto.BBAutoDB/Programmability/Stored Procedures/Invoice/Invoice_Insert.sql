create procedure [dbo].[Invoice_Insert]
  @idInvoice int,
  @idCar int,
  @number int,
  @idDriverFrom int,
  @idDriverTo int,
  @date datetime,
  @dateMoveText varchar(50),
  @idRegionFrom int,
  @idRegionTo int,
  @file varchar(500),
  @IsMain bit
as
begin
  declare @dateMove datetime

  if (@dateMoveText = '')
    set @dateMove = null
  else
    set @dateMove = cast(@dateMoveText as datetime)

  if (@idInvoice = 0)
  begin
    select
      @number = max(invoice_number) + 1
    from
      Invoice
    where
      year(invoice_date) = year(@date)

    if (@number is null)
      set @number = 1

    insert into Invoice values(@idCar, @number, @idDriverFrom, @idDriverTo, @date, @dateMove, @idRegionFrom, @idRegionTo, @file, @IsMain)
    set @idInvoice = @@IDENTITY
  end
  else
  begin
    update
      Invoice
    set
      driver_id_From = @idDriverFrom,
      driver_id_To = @idDriverTo,
      invoice_date = @date,
      invoice_dateMove = @dateMove,
      region_id_From = @idRegionFrom,
      region_id_To = @idRegionTo,
      invoice_file = @file,
      IsMain = @IsMain
    where
      invoice_id = @idInvoice
  end

  select @idInvoice
end
