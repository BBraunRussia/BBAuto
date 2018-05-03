create procedure [dbo].[UpsertStatusAfterDTP]
  @id int,
  @Name nvarchar(50)
as
begin
  if (@id = 0)
  begin
    declare @count int
    select
      @count = count(StatusAfterDTP_id)
    from
      StatusAfterDTP
    where
      StatusAfterDTP_name = @Name

    if (@count = 0)
    begin
      insert into StatusAfterDTP values(@Name)
      select 'Добавлен'
    end
    else
      select 'Данный статус уже существует'
  end
  else
  begin
    update StatusAfterDTP
    set StatusAfterDTP_name = @Name
    where StatusAfterDTP_id = @id
    select 'Обновлен'
  end
end
