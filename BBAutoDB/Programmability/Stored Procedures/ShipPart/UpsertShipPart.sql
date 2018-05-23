create procedure [dbo].[UpsertShipPart]
  @id int,
  @CarId int,
  @DriverId int,
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
    insert into ShipPart values(@CarId, @DriverId, @name, @dateRequest, @dateSent, @file)

    set @id = scope_identity()
  end
  else
  begin
    update ShipPart
    set DriverId = @DriverId,
        [Name] = @name,
        DateRequest = @dateRequest,
        DateSent = @dateSent,
        [File] = @file
    where Id = @id
  end

  select @id
end
