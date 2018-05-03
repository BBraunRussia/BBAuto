create procedure [dbo].[UpsertCulprit]
  @id int,
  @Name nvarchar(50)
as
begin
  if (@id = 0)
  begin
    declare @count int
    select
      @count = count(culprit_id)
    from
      Culprit
    where
      culprit_name = @Name

    if (@count = 0)
    begin
      insert into Culprit values(@Name)
      select 'Добавлен'
    end
    else
      select 'Данный виновник уже существует'
  end
  else
  begin
    update Culprit
    set culprit_name = @Name
    where culprit_id = @id
    select 'Обновлен'
  end
end
