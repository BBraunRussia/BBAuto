create procedure [dbo].[UpsertOwner]
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
      [Owner]
    where
      [Name] = @Name

    if (@count = 0)
    begin
      insert into Owner values(@Name)
      select 'Добавлен'
    end
    else
      select 'Данный владелец уже существует'
  end
  else
  begin
    update Owner
    set [Name] = @Name
    where Id = @id
    select 'Обновлен'
  end
end
