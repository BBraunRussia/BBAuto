create procedure [dbo].[UpsertRepairType]
  @id int,
  @Name nvarchar(50)
as
begin
  if (@id = 0)
  begin
    declare @count int
    select
      @count = count(repairType_id)
    from
      RepairType
    where
      repairType_name = @Name

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
    set repairType_name = @Name
    where repairType_id = @id
    select 'Обновлен'
  end
end
