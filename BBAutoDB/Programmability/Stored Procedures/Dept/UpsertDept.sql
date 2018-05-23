create procedure [dbo].[UpsertDept]
  @id int,
  @Name nvarchar(100)
as
begin
  if (@id = 0)
  begin
    declare @count int
    select
      @count = count(*)
    from
      Dept
    where
      [Name] = @Name

    if (@count = 0)
    begin
      insert into Dept values(@Name)
      select 'Добавлен'
    end
    else
      select 'Данный департамент уже существует'
  end
  else
  begin
    update Dept
    set [Name] = @Name
    where Id = @id
    select 'Обновлен'
  end
end
