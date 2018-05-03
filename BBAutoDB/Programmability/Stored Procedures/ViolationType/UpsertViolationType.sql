create procedure [dbo].[UpsertViolationType]
  @id int,
  @Name nvarchar(50)
as
begin
  if (@id = 0)
  begin
    declare @count int
    select
      @count = count(violationType_id)
    from
      ViolationType
    where
      violationType_name = @Name

    if (@count = 0)
    begin
      insert into ViolationType values(@Name)
      select 'Добавлен'
    end
    else
      select 'Данный тип нарушения уже существует'
  end
  else
  begin
    update ViolationType
    set violationType_name = @Name
    where violationType_id = @id
    select 'Обновлен'
  end
end
