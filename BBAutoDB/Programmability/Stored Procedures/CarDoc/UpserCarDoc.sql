create procedure [dbo].[UpserCarDoc]
  @id int,
  @idCar int,
  @name nvarchar(50),
  @file nvarchar(200)
as
begin
  begin transaction
  if (@id = 0)
  begin
    declare @count int
    select
      @count = count(Id)
    from
      CarDoc
    where
      [Name] = @name
      and [File] = @file
      and CarId = @idCar

    if (@count = 0 and @idCar != 0)
    begin
      insert into CarDoc values(@idCar, @name, @file)

      select @id = scope_identity()

      if @@error <> 0
        rollback transaction
    end
  end
  else
  begin
    update
      CarDoc
    set
      [Name] = @name,
      [File] = @file
    where
      Id = @id

    if @@error <> 0
      rollback transaction
  end
  commit transaction;

  exec dbo.GetCardGetCarDocById @id
end
