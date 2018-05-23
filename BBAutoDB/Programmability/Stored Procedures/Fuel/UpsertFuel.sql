create procedure [dbo].[UpsertFuel]
  @FuelCardId int,
  @date datetime,
  @count float,
  @EngineTypeId int
as
begin
  declare @id int

  select
    @id = Id
  from
    Fuel
  where
    FuelCardId = @FuelCardId
    and [Date] = @date
    and EngineTypeId = @EngineTypeId

  if (@id is null)
  begin
    insert into Fuel values(@FuelCardId, @date, @count, @EngineTypeId)

    set @id = scope_identity()
  end
  else
  begin
    update Fuel
    set [Count] = [Count] + @count
    where Id = @id
  end

  select @id
end
