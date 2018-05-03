create procedure [dbo].[UpsertShipPart]
  @id int,
  @idCar int,
  @idDriver int,
  @name nvarchar(50),
  @dateRequestText nvarchar(50),
  @dateSentText nvarchar(50),
  @file nvarchar(500)
as
begin
  declare @dateRequest datetime

  if (@dateRequestText = '')
    set @dateRequest = null
  else
    set @dateRequest = cast(@dateRequestText as datetime)

  declare @dateSent datetime

  if (@dateSentText = '')
    set @dateSent = null
  else
    set @dateSent = cast(@dateSentText as datetime)

  if (@id = 0)
  begin
    insert into ShipPart values(@idCar, @idDriver, @name, @dateRequest, @dateSent, @file)

    set @id = scope_identity()
  end
  else
  begin
    update ShipPart
    set driver_id = @idDriver,
        shipPart_name = @name,
        shipPart_dateRequest = @dateRequest,
        shipPart_dateSent = @dateSent,
        shipPart_file = @file
    where shipPart_id = @id
  end

  select @id
end
