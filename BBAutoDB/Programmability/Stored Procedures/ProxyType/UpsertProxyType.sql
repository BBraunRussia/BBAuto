create procedure [dbo].[UpsertProxyType]
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
      proxyType
    where
      [Name] = @Name

    if (@count = 0)
    begin
      insert into proxyType values(@Name)
      select 'Добавлен'
    end
    else
      select 'Данный тип доверенности уже существует'
  end
  else
  begin
    update proxyType
    set [Name] = @Name
    where Id = @id
    select 'Обновлен'
  end
end
