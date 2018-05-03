create procedure [dbo].[UpsertFuel]
  @idFuelCard int,
  @date datetime,
  @count float,
  @idEngineType int
as
begin
  declare @id int

  select
    @id = fuel_id
  from
    Fuel
  where
    fuelCard_id = @idFuelCard
    and fuel_date = @date
    and engineType_id = @idEngineType

  if (@id is null)
  begin
    insert into Fuel values(@idFuelCard, @date, @count, @idEngineType)

    set @id = scope_identity()
  end
  else
  begin
    update Fuel
    set fuel_count = fuel_count + @count
    where fuel_id = @id
  end

  select @id
end
