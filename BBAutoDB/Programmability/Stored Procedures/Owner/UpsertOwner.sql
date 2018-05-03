create procedure [dbo].[UpsertOwner]
  @id int,
  @Name nvarchar(50)
as
begin
  if (@id = 0)
  begin
    declare @count int
    select
      @count = count(owner_id)
    from
      Owner
    where
      owner_name = @Name

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
    set owner_name = @Name
    where owner_id = @id
    select 'Обновлен'
  end
end
