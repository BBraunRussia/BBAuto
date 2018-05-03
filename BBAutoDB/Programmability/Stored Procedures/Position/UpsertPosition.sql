create procedure [dbo].[UpsertPosition]
  @id int,
  @Name nvarchar(100)
as
begin
  if (@id = 0)
  begin
    declare @count int
    select
      @count = count(position_id)
    from
      Position
    where
      position_name = @Name

    if (@count = 0)
    begin
      insert into Position values(@Name)
      select 'Добавлен'
    end
    else
      select 'Данная должность уже существует'
  end
  else
  begin
    update Position
    set position_name = @Name
    where position_id = @id
    select 'Обновлен'
  end
end
