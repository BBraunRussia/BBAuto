create procedure [dbo].[UpsertEngineType]
  @id int,
  @Name nvarchar(50),
  @ShortName nvarchar(50)
as
begin
  if (@id = 0)
  begin
    declare @count int
    select
      @count = count(*)
    from
      EngineType
    where
      [Name] = @Name

    if (@count = 0)
    begin
      insert into EngineType values(@Name, @ShortName)
      select 'Добавлен'
    end
    else
      select 'Такой тип двигателя уже существует'
  end
  else
  begin
    update EngineType
    set [Name] = @Name,
        ShortName = @ShortName
    where Id = @id

    select 'Обновлен'
  end
end
