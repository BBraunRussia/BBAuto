create procedure [dbo].[UpsertComp]
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
      Comp
    where
      [Name] = @Name

    if (@count = 0)
    begin
      insert into Comp values(@Name)
      select 'Добавлен'
    end
    else
      select 'Данная компания уже существует'
  end
  else
  begin
    update Comp
    set [Name] = @Name
    where Id = @id
    select 'Обновлен'
  end
end
