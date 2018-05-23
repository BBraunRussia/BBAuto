create procedure [dbo].[UpsertCulprit]
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
      Culprit
    where
      [Name] = @Name

    if (@count = 0)
    begin
      insert into Culprit values(@Name)
      select 'Добавлен'
    end
    else
      select 'Данный виновник уже существует'
  end
  else
  begin
    update Culprit
    set [Name] = @Name
    where Id = @id
    select 'Обновлен'
  end
end
