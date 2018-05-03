create procedure [dbo].[UpsertComp]
  @id int,
  @Name nvarchar(50)
as
begin
  if (@id = 0)
  begin
    declare @count int
    select
      @count = count(comp_id)
    from
      Comp
    where
      comp_name = @Name

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
    set comp_name = @Name
    where comp_id = @id
    select 'Обновлен'
  end
end
