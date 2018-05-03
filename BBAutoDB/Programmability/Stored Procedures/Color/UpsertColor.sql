create procedure [dbo].[UpsertColor]
  @id int,
  @Name nvarchar(50)
as
begin
  if (@id = 0)
  begin
    declare @count int
    select
      @count = count(color_id)
    from
      Color
    where
      color_name = @Name

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
    set color_name = @Name
    where color_id = @id
    select 'Обновлен'
  end
end
