create procedure [dbo].[UpsertFuelCardType]
  @idFuelCardType int,
  @name nvarchar(50)
as
begin
  if (@idFuelCardType = 0)
  begin
    insert into FuelCardType values(@name)

    set @idFuelCardType = scope_identity()
  end
  else
    update FuelCardType
    set FuelCardType_name = @name

  select @idFuelCardType
end
