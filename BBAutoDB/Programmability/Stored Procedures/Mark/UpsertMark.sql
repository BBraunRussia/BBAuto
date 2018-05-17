create procedure [dbo].[UpsertMark]
  @id int,
  @Name nvarchar(50)
as
begin
  if (@id = 0)
  begin
    declare @count int
    select
      @count = count(Id)
    from
      Mark
    where
      [Name] = @Name

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
    set [Name] = @Name
    where Id = @id

    select 'Обновлен'
  end
end
