create procedure [dbo].[UpsertRepairType]
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
      RepairType
    where
      [Name] = @Name

    if (@count = 0)
    begin
      insert into RepairType values(@Name)
      select 'Добавлен'
    end
    else
      select 'Данный вид ремонта уже существует'
  end
  else
  begin
    update RepairType
    set [Name] = @Name
    where Id = @id
    select 'Обновлен'
  end
end
