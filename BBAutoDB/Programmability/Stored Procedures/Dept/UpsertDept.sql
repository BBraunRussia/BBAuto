create procedure [dbo].[UpsertDept]
  @id int,
  @Name nvarchar(100)
as
begin
  if (@id = 0)
  begin
    declare @count int
    select
      @count = count(dept_id)
    from
      Dept
    where
      dept_name = @Name

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
    set dept_name = @Name
    where dept_id = @id
    select 'Обновлен'
  end
end
