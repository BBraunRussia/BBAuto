create procedure [dbo].[UpserCarDoc]
  @idCarDoc int,
  @idCar int,
  @name nvarchar(50),
  @file nvarchar(200)
as
begin
  begin transaction
  if (@idCarDoc = 0)
  begin
    declare @count int
    select
      @count = count(carDoc_id)
    from
      carDoc
    where
      carDoc_name = @name
      and carDoc_file = @file
      and car_id = @idCar

    if (@count = 0
      and @idCar != 0)
    begin
      insert into carDoc values(@idCar, @name, @file)

      select @idCarDoc = @@identity

      if @@error <> 0
        rollback transaction
    end
  end
  else
  begin
    update carDoc
    set carDoc_name = @name,
        carDoc_file = @file
    where carDoc_id = @idCarDoc

    if @@error <> 0
      rollback transaction
  end
  commit transaction;

  select @idCarDoc
end
