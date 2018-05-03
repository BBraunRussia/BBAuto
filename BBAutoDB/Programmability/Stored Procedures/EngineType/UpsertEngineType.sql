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
      @count = count(engineType_id)
    from
      EngineType
    where
      engineType_name = @Name

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
    set engineType_name = @Name,
        engineType_shortName = @ShortName
    where engineType_id = @id

    select 'Обновлен'
  end
end
