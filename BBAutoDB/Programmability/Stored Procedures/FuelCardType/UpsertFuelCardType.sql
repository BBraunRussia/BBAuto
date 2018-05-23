create procedure [dbo].[UpsertFuelCardType]
  @id int,
  @name nvarchar(50)
as
begin
  if (@id = 0)
  begin
    insert into FuelCardType values(@name)

    set @id = scope_identity()
  end
  else
    update FuelCardType
    set [Name] = @name

  select @id
end
