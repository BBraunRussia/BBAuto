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
      @count = count(model_id)
    from
      Model
    where
      model_name = @Name
      and mark_id = @idMark

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
    set model_name = @Name,
        mark_id = @idMark
    where model_id = @id
    select 'Обновлен'
  end
end
