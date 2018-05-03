create procedure [dbo].[UpsertMark]
  @id int,
  @Name nvarchar(50)
as
begin
  if (@id = 0)
  begin
    declare @count int
    select
      @count = count(mark_id)
    from
      Mark
    where
      mark_name = @Name

    if (@count = 0)
    begin
      insert into Mark values(@Name)
      select 'Добавлен'
    end
    else
      select 'Марка автомобиля с таким названием уже существует'
  end
  else
  begin
    update Mark
    set mark_name = @Name
    where mark_id = @id
    select 'Обновлен'
  end
end
