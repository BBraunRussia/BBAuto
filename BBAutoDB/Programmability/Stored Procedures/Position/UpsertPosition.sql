create procedure [dbo].[UpsertPosition]
  @id int,
  @Name nvarchar(100)
as
begin
  if (@id = 0)
  begin
    declare @count int
    select
      @count = count(*)
    from
      Position
    where
      [Name] = @Name

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
    set [Name] = @Name
    where Id = @id
    select 'Обновлен'
  end
end
