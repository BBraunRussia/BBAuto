create procedure [dbo].[UpsertColor]
  @id int,
  @Name nvarchar(50)
as
begin
  if (@id = 0)
  begin
    declare @count int
    select
      @count = count(*)
    from
      Color
    where
      [Name] = @Name

    if (@count = 0)
    begin
      insert into Color values(@Name)
      select 'Добавлен'
    end
    else
      select 'Данный цвет кузова уже существует'
  end
  else
  begin
    update Color
    set [Name] = @Name
    where Id = @id
    select 'Обновлен'
  end
end
