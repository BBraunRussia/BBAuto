create procedure [dbo].[UpsertStatusAfterDTP]
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
      StatusAfterDTP
    where
      [Name] = @Name

    if (@count = 0)
    begin
      insert into StatusAfterDTP values(@Name)
      select 'Добавлен'
    end
    else
      select 'Данный статус уже существует'
  end
  else
  begin
    update StatusAfterDTP
    set [Name] = @Name
    where Id = @id
    select 'Обновлен'
  end
end
