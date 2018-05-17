create procedure [dbo].[UpsertModel]
  @id int,
  @Name nvarchar(50),
  @idMark int
as
begin
  if (@id = 0)
  begin
    declare @count int
    select
      @count = count(Id)
    from
      Model
    where
      [Name] = @Name
      and MarkId = @idMark

    if (@count = 0)
    begin
      insert into Model values(@Name, @idMark)
      select 'Добавлен'
    end
    else
      select 'Модель автомобиля с таким названием уже существует'
  end
  else
  begin
    update Model
    set [Name] = @Name,
        MarkId = @idMark
    where Id = @id
    select 'Обновлен'
  end
end
